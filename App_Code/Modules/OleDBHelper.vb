Imports System.Data.OleDb
Imports System.Data

' The OleDBHelper class is intended to encapsulate high performance, scalable best practices for 
' common uses of OleDBClient.

' ===============================================================================
' Release history
' VERSION	DESCRIPTION
'   2.0	Added support for FillDataset, UpdateDataset and "Param" helper methods
'
' ===============================================================================

Public NotInheritable Class OleDBHelper

#Region "private utility methods & constructors"

    ' Since this class provides only static methods, make the default constructor private to prevent 
    ' instances from being created with "new OleDBHelper()".
    Private Sub New()
    End Sub ' New

    ' This method is used to attach array of OleDBParameters to a OleDBCommand.
    ' This method will assign a value of DbNull to any parameter with a direction of
    ' InputOutput and a value of null.  
    ' This behavior will prevent default values from being used, but
    ' this will be the less common case than an intended pure output parameter (derived as InputOutput)
    ' where the user provided no input value.
    ' Parameters:
    ' -command - The command to which the parameters will be added
    ' -commandParameters - an array of OleDBParameters to be added to command
    Private Shared Sub AttachParameters(ByVal command As OleDbCommand, ByVal commandParameters() As OleDbParameter)
        If (command Is Nothing) Then Throw New ArgumentNullException("command")
        If (Not commandParameters Is Nothing) Then
            Dim p As OleDbParameter
            For Each p In commandParameters
                If (Not p Is Nothing) Then
                    ' Check for derived output value with no value assigned
                    If (p.Direction = ParameterDirection.InputOutput OrElse p.Direction = ParameterDirection.Input) AndAlso p.Value Is Nothing Then
                        p.Value = DBNull.Value
                    End If
                    command.Parameters.Add(p)
                End If
            Next p
        End If
    End Sub ' AttachParameters

    ' This method assigns dataRow column values to an array of OleDBParameters.
    ' Parameters:
    ' -commandParameters: Array of OleDBParameters to be assigned values
    ' -dataRow: the dataRow used to hold the stored procedure' s parameter values
    Private Overloads Shared Sub AssignParameterValues(ByVal commandParameters() As OleDbParameter, ByVal dataRow As DataRow)

        If commandParameters Is Nothing OrElse dataRow Is Nothing Then
            ' Do nothing if we get no data    
            Exit Sub
        End If

        ' Set the parameters values
        Dim commandParameter As OleDbParameter
        Dim i As Integer
        For Each commandParameter In commandParameters
            ' Check the parameter name
            If (commandParameter.ParameterName Is Nothing OrElse commandParameter.ParameterName.Length <= 1) Then
                Throw New Exception(String.Format("Please provide a valid parameter name on the parameter #{0}, the ParameterName property has the following value: ' {1}' .", i, commandParameter.ParameterName))
            End If
            If dataRow.Table.Columns.IndexOf(commandParameter.ParameterName.Substring(1)) <> -1 Then
                commandParameter.Value = dataRow(commandParameter.ParameterName.Substring(1))
            End If
            i = i + 1
        Next
    End Sub

    ' This method assigns an array of values to an array of OleDBParameters.
    ' Parameters:
    ' -commandParameters - array of OleDBParameters to be assigned values
    ' -array of objects holding the values to be assigned
    Private Overloads Shared Sub AssignParameterValues(ByVal commandParameters() As OleDbParameter, ByVal parameterValues() As Object)

        Dim i As Integer
        Dim j As Integer

        If (commandParameters Is Nothing) AndAlso (parameterValues Is Nothing) Then
            ' Do nothing if we get no data
            Return
        End If

        ' We must have the same number of values as we pave parameters to put them in
        If commandParameters.Length <> parameterValues.Length Then
            Throw New ArgumentException("Parameter count does not match Parameter Value count.")
        End If

        ' Value array
        j = commandParameters.Length - 1
        For i = 0 To j
            ' If the current array value derives from IDbDataParameter, then assign its Value property
            If TypeOf parameterValues(i) Is IDbDataParameter Then
                Dim paramInstance As IDbDataParameter = CType(parameterValues(i), IDbDataParameter)
                If (paramInstance.Value Is Nothing) Then
                    commandParameters(i).Value = DBNull.Value
                Else
                    commandParameters(i).Value = paramInstance.Value
                End If
            ElseIf (parameterValues(i) Is Nothing) Then
                commandParameters(i).Value = DBNull.Value
            Else
                commandParameters(i).Value = parameterValues(i)
            End If
        Next
    End Sub ' AssignParameterValues

    ' This method opens (if necessary) and assigns a connection, transaction, command type and parameters 
    ' to the provided command.
    ' Parameters:
    ' -command - the OleDBCommand to be prepared
    ' -connection - a valid OleDBConnection, on which to execute this command
    ' -transaction - a valid OleDBTransaction, or ' null' 
    ' -commandType - the CommandType (stored procedure, text, etc.)
    ' -commandText - the stored procedure name or T-OleDB command
    ' -commandParameters - an array of OleDBParameters to be associated with the command or ' null' if no parameters are required
    Private Shared Sub PrepareCommand(ByVal command As OleDbCommand, _
                                      ByVal connection As OleDbConnection, _
                                      ByVal transaction As OleDbTransaction, _
                                      ByVal commandType As CommandType, _
                                      ByVal commandText As String, _
                                      ByVal commandParameters() As OleDbParameter, ByRef mustCloseConnection As Boolean)

        If (command Is Nothing) Then Throw New ArgumentNullException("command")
        If (commandText Is Nothing OrElse commandText.Length = 0) Then Throw New ArgumentNullException("commandText")

        ' If the provided connection is not open, we will open it
        If connection.State <> ConnectionState.Open Then
            connection.Open()
            mustCloseConnection = True
        Else
            mustCloseConnection = False
        End If

        ' Associate the connection with the command
        command.Connection = connection

        ' Set the command text (stored procedure name or OleDB statement)
        command.CommandText = commandText

        ' If we were provided a transaction, assign it.
        If Not (transaction Is Nothing) Then
            If transaction.Connection Is Nothing Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            command.Transaction = transaction
        End If

        ' Set the command type
        command.CommandType = commandType

        ' Attach the command parameters if they are provided
        If Not (commandParameters Is Nothing) Then
            AttachParameters(command, commandParameters)
        End If
        Return
    End Sub ' PrepareCommand

#End Region

#Region "ExecuteNonQuery"

    ' Execute a OleDBCommand (that returns no resultset and takes no parameters) against the database specified in 
    ' the connection string. 
    ' e.g.:  
    '  Dim result As Integer =  ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders")
    ' Parameters:
    ' -connectionString - a valid connection string for a OleDBConnection
    ' -commandType - the CommandType (stored procedure, text, etc.)
    ' -commandText - the stored procedure name or T-OleDB command
    ' Returns: An int representing the number of rows affected by the command
    Public Overloads Shared Function ExecuteNonQuery(ByVal connectionString As String, _
                                                   ByVal commandType As CommandType, _
                                                   ByVal commandText As String) As Integer
        ' Pass through the call providing null for the set of OleDBParameters
        Return ExecuteNonQuery(connectionString, commandType, commandText, CType(Nothing, OleDbParameter()))
    End Function ' ExecuteNonQuery

    ' Execute a OleDBCommand (that returns no resultset) against the database specified in the connection string 
    ' using the provided parameters.
    ' e.g.:  
    ' Dim result As Integer = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new OleDBParameter("@prodid", 24))
    ' Parameters:
    ' -connectionString - a valid connection string for a OleDBConnection
    ' -commandType - the CommandType (stored procedure, text, etc.)
    ' -commandText - the stored procedure name or T-OleDB command
    ' -commandParameters - an array of OleDBParamters used to execute the command
    ' Returns: An int representing the number of rows affected by the command
    Public Overloads Shared Function ExecuteNonQuery(ByVal connectionString As String, _
                                                     ByVal commandType As CommandType, _
                                                     ByVal commandText As String, _
                                                     ByVal ParamArray commandParameters() As OleDbParameter) As Integer
        If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
        ' Create & open a OleDBConnection, and dispose of it after we are done
        Dim connection As OleDbConnection
        Try
            connection = New OleDbConnection(connectionString)
            connection.Open()

            ' Call the overload that takes a connection in place of the connection string
            Return ExecuteNonQuery(connection, commandType, commandText, commandParameters)
        Finally
            If Not connection Is Nothing Then connection.Dispose()
        End Try
    End Function ' ExecuteNonQuery

    ' Execute a OleDBCommand (that returns no resultset and takes no parameters) against the provided OleDBConnection. 
    ' e.g.:  
    ' Dim result As Integer = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders")
    ' Parameters:
    ' -connection - a valid OleDBConnection
    ' -commandType - the CommandType (stored procedure, text, etc.)
    ' -commandText - the stored procedure name or T-OleDB command 
    ' Returns: An int representing the number of rows affected by the command
    Public Overloads Shared Function ExecuteNonQuery(ByVal connection As OleDbConnection, _
                                                     ByVal commandType As CommandType, _
                                                     ByVal commandText As String) As Integer
        ' Pass through the call providing null for the set of OleDBParameters
        Return ExecuteNonQuery(connection, commandType, commandText, CType(Nothing, OleDbParameter()))

    End Function ' ExecuteNonQuery

    ' Execute a OleDBCommand (that returns no resultset) against the specified OleDBConnection 
    ' using the provided parameters.
    ' e.g.:  
    '  Dim result As Integer = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders", new OleDBParameter("@prodid", 24))
    ' Parameters:
    ' -connection - a valid OleDBConnection 
    ' -commandType - the CommandType (stored procedure, text, etc.)
    ' -commandText - the stored procedure name or T-OleDB command 
    ' -commandParameters - an array of OleDBParamters used to execute the command 
    ' Returns: An int representing the number of rows affected by the command 
    Public Overloads Shared Function ExecuteNonQuery(ByVal connection As OleDbConnection, _
                                                     ByVal commandType As CommandType, _
                                                     ByVal commandText As String, _
                                                     ByVal ParamArray commandParameters() As OleDbParameter) As Integer

        If (connection Is Nothing) Then Throw New ArgumentNullException("connection")

        ' Create a command and prepare it for execution
        Dim cmd As New OleDbCommand
        Dim retval As Integer
        Dim mustCloseConnection As Boolean = False

        PrepareCommand(cmd, connection, CType(Nothing, OleDbTransaction), commandType, commandText, commandParameters, mustCloseConnection)

        ' Finally, execute the command
        retval = cmd.ExecuteNonQuery()

        If (mustCloseConnection) Then connection.Close()

        Return retval
    End Function ' ExecuteNonQuery

    ' Execute a OleDBCommand (that returns no resultset and takes no parameters) against the provided OleDBTransaction.
    ' e.g.:  
    '  Dim result As Integer = ExecuteNonQuery(trans, CommandType.StoredProcedure, "PublishOrders")
    ' Parameters:
    ' -transaction - a valid OleDBTransaction associated with the connection 
    ' -commandType - the CommandType (stored procedure, text, etc.) 
    ' -commandText - the stored procedure name or T-OleDB command 
    ' Returns: An int representing the number of rows affected by the command 
    Public Overloads Shared Function ExecuteNonQuery(ByVal transaction As OleDbTransaction, _
                                                     ByVal commandType As CommandType, _
                                                     ByVal commandText As String) As Integer
        ' Pass through the call providing null for the set of OleDBParameters
        Return ExecuteNonQuery(transaction, commandType, commandText, CType(Nothing, OleDbParameter()))
    End Function ' ExecuteNonQuery

    ' Execute a OleDBCommand (that returns no resultset) against the specified OleDBTransaction
    ' using the provided parameters.
    ' e.g.:  
    ' Dim result As Integer = ExecuteNonQuery(trans, CommandType.StoredProcedure, "GetOrders", new OleDBParameter("@prodid", 24))
    ' Parameters:
    ' -transaction - a valid OleDBTransaction 
    ' -commandType - the CommandType (stored procedure, text, etc.) 
    ' -commandText - the stored procedure name or T-OleDB command 
    ' -commandParameters - an array of OleDBParamters used to execute the command 
    ' Returns: An int representing the number of rows affected by the command 
    Public Overloads Shared Function ExecuteNonQuery(ByVal transaction As OleDbTransaction, _
                                                     ByVal commandType As CommandType, _
                                                     ByVal commandText As String, _
                                                     ByVal ParamArray commandParameters() As OleDbParameter) As Integer

        If (transaction Is Nothing) Then Throw New ArgumentNullException("transaction")
        If Not (transaction Is Nothing) AndAlso (transaction.Connection Is Nothing) Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")

        ' Create a command and prepare it for execution
        Dim cmd As New OleDbCommand
        Dim retval As Integer
        Dim mustCloseConnection As Boolean = False

        PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, mustCloseConnection)

        ' Finally, execute the command
        retval = cmd.ExecuteNonQuery()

        Return retval
    End Function ' ExecuteNonQuery

#End Region

#Region "ExecuteDataset"

    ' Execute a OleDBCommand (that returns a resultset and takes no parameters) against the database specified in 
    ' the connection string. 
    ' e.g.:  
    ' Dim ds As DataSet = OleDBHelper.ExecuteDataset("", commandType.StoredProcedure, "GetOrders")
    ' Parameters:
    ' -connectionString - a valid connection string for a OleDBConnection
    ' -commandType - the CommandType (stored procedure, text, etc.)
    ' -commandText - the stored procedure name or T-OleDB command
    ' Returns: A dataset containing the resultset generated by the command
    Public Overloads Shared Function ExecuteDataset(ByVal connectionString As String, _
                                                    ByVal commandType As CommandType, _
                                                    ByVal commandText As String) As DataSet
        ' Pass through the call providing null for the set of OleDBParameters
        Return ExecuteDataset(connectionString, commandType, commandText, CType(Nothing, OleDbParameter()))
    End Function ' ExecuteDataset

    ' Execute a OleDBCommand (that returns a resultset) against the database specified in the connection string 
    ' using the provided parameters.
    ' e.g.:  
    ' Dim ds As Dataset = ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders", new OleDBParameter("@prodid", 24))
    ' Parameters:
    ' -connectionString - a valid connection string for a OleDBConnection
    ' -commandType - the CommandType (stored procedure, text, etc.)
    ' -commandText - the stored procedure name or T-OleDB command
    ' -commandParameters - an array of OleDBParamters used to execute the command
    ' Returns: A dataset containing the resultset generated by the command
    Public Overloads Shared Function ExecuteDataset(ByVal connectionString As String, _
                                                    ByVal commandType As CommandType, _
                                                    ByVal commandText As String, _
                                                    ByVal ParamArray commandParameters() As OleDbParameter) As DataSet

        If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")

        ' Create & open a OleDBConnection, and dispose of it after we are done
        Dim connection As OleDbConnection
        Try

            connection = New OleDbConnection(connectionString)
            connection.Open()

            ' Call the overload that takes a connection in place of the connection string
            Return ExecuteDataset(connection, commandType, commandText, commandParameters)
        Catch ex As Exception
            Return Nothing
        Finally
            If Not connection Is Nothing Then connection.Dispose()
        End Try

    End Function ' ExecuteDataset

    ' Execute a OleDBCommand (that returns a resultset and takes no parameters) against the provided OleDBConnection. 
    ' e.g.:  
    ' Dim ds As Dataset = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders")
    ' Parameters:
    ' -connection - a valid OleDBConnection
    ' -commandType - the CommandType (stored procedure, text, etc.)
    ' -commandText - the stored procedure name or T-OleDB command
    ' Returns: A dataset containing the resultset generated by the command
    Public Overloads Shared Function ExecuteDataset(ByVal connection As OleDbConnection, _
                                                    ByVal commandType As CommandType, _
                                                    ByVal commandText As String) As DataSet

        ' Pass through the call providing null for the set of OleDBParameters
        Return ExecuteDataset(connection, commandType, commandText, CType(Nothing, OleDbParameter()))
    End Function ' ExecuteDataset

    ' Execute a OleDBCommand (that returns a resultset) against the specified OleDBConnection 
    ' using the provided parameters.
    ' e.g.:  
    ' Dim ds As Dataset = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders", new OleDBParameter("@prodid", 24))
    ' Parameters:
    ' -connection - a valid OleDBConnection
    ' -commandType - the CommandType (stored procedure, text, etc.)
    ' -commandText - the stored procedure name or T-OleDB command
    ' -commandParameters - an array of OleDBParamters used to execute the command
    ' Returns: A dataset containing the resultset generated by the command
    Public Overloads Shared Function ExecuteDataset(ByVal connection As OleDbConnection, _
                                                    ByVal commandType As CommandType, _
                                                    ByVal commandText As String, _
                                                    ByVal ParamArray commandParameters() As OleDbParameter) As DataSet
        If (connection Is Nothing) Then Throw New ArgumentNullException("connection")
        ' Create a command and prepare it for execution
        Dim cmd As New OleDbCommand
        Dim ds As New DataSet
        Dim dataAdatpter As OleDbDataAdapter
        Dim mustCloseConnection As Boolean = False

        PrepareCommand(cmd, connection, CType(Nothing, OleDbTransaction), commandType, commandText, commandParameters, mustCloseConnection)

        Try
            ' Create the DataAdapter & DataSet
            dataAdatpter = New OleDbDataAdapter(cmd)

            ' Fill the DataSet using default values for DataTable names, etc
            dataAdatpter.Fill(ds)
        Finally
            If (Not dataAdatpter Is Nothing) Then dataAdatpter.Dispose()
        End Try
        If (mustCloseConnection) Then connection.Close()

        ' Return the dataset
        Return ds
    End Function ' ExecuteDataset

    ' Execute a OleDBCommand (that returns a resultset and takes no parameters) against the provided OleDBTransaction. 
    ' e.g.:  
    ' Dim ds As Dataset = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders")
    ' Parameters
    ' -transaction - a valid OleDBTransaction
    ' -commandType - the CommandType (stored procedure, text, etc.)
    ' -commandText - the stored procedure name or T-OleDB command
    ' Returns: A dataset containing the resultset generated by the command
    Public Overloads Shared Function ExecuteDataset(ByVal transaction As OleDbTransaction, _
                                                    ByVal commandType As CommandType, _
                                                    ByVal commandText As String) As DataSet
        ' Pass through the call providing null for the set of OleDBParameters
        Return ExecuteDataset(transaction, commandType, commandText, CType(Nothing, OleDbParameter()))
    End Function ' ExecuteDataset

    ' Execute a OleDBCommand (that returns a resultset) against the specified OleDBTransaction
    ' using the provided parameters.
    ' e.g.:  
    ' Dim ds As Dataset = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders", new OleDBParameter("@prodid", 24))
    ' Parameters
    ' -transaction - a valid OleDBTransaction 
    ' -commandType - the CommandType (stored procedure, text, etc.)
    ' -commandText - the stored procedure name or T-OleDB command
    ' -commandParameters - an array of OleDBParamters used to execute the command
    ' Returns: A dataset containing the resultset generated by the command
    Public Overloads Shared Function ExecuteDataset(ByVal transaction As OleDbTransaction, _
                                                    ByVal commandType As CommandType, _
                                                    ByVal commandText As String, _
                                                    ByVal ParamArray commandParameters() As OleDbParameter) As DataSet
        If (transaction Is Nothing) Then Throw New ArgumentNullException("transaction")
        If Not (transaction Is Nothing) AndAlso (transaction.Connection Is Nothing) Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")

        ' Create a command and prepare it for execution
        Dim cmd As New OleDbCommand
        Dim ds As New DataSet
        Dim dataAdatpter As OleDbDataAdapter
        Dim mustCloseConnection As Boolean = False

        PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, mustCloseConnection)

        Try
            ' Create the DataAdapter & DataSet
            dataAdatpter = New OleDbDataAdapter(cmd)

            ' Fill the DataSet using default values for DataTable names, etc
            dataAdatpter.Fill(ds)
        Finally
            If (Not dataAdatpter Is Nothing) Then dataAdatpter.Dispose()
        End Try

        ' Return the dataset
        Return ds

    End Function ' ExecuteDataset

#End Region

#Region "ExecuteReader"
    ' this enum is used to indicate whether the connection was provided by the caller, or created by OleDBHelper, so that
    ' we can set the appropriate CommandBehavior when calling ExecuteReader()
    Private Enum OleDBConnectionOwnership
        ' Connection is owned and managed by OleDBHelper
        Internal
        ' Connection is owned and managed by the caller
        [External]
    End Enum ' OleDBConnectionOwnership

    ' Create and prepare a OleDBCommand, and call ExecuteReader with the appropriate CommandBehavior.
    ' If we created and opened the connection, we want the connection to be closed when the DataReader is closed.
    ' If the caller provided the connection, we want to leave it to them to manage.
    ' Parameters:
    ' -connection - a valid OleDBConnection, on which to execute this command 
    ' -transaction - a valid OleDBTransaction, or ' null' 
    ' -commandType - the CommandType (stored procedure, text, etc.) 
    ' -commandText - the stored procedure name or SQL command 
    ' -commandParameters - an array of OleDBParameters to be associated with the command or ' null' if no parameters are required 
    ' -connectionOwnership - indicates whether the connection parameter was provided by the caller, or created by OleDBHelper 
    ' Returns: OleDBDataReader containing the results of the command 
    Private Overloads Shared Function ExecuteReader(ByVal connection As OleDbConnection, _
                                                    ByVal transaction As OleDbTransaction, _
                                                    ByVal commandType As CommandType, _
                                                    ByVal commandText As String, _
                                                    ByVal commandParameters() As OleDbParameter, _
                                                    ByVal connectionOwnership As OleDBConnectionOwnership) As OleDbDataReader

        If (connection Is Nothing) Then Throw New ArgumentNullException("connection")

        Dim mustCloseConnection As Boolean = False
        ' Create a command and prepare it for execution
        Dim cmd As New OleDbCommand
        Try
            ' Create a reader
            Dim dataReader As OleDbDataReader

            PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters, mustCloseConnection)

            ' Call ExecuteReader with the appropriate CommandBehavior
            If connectionOwnership = OleDBConnectionOwnership.External Then
                dataReader = cmd.ExecuteReader()
            Else
                dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            End If

            Return dataReader
        Catch
            If (mustCloseConnection) Then connection.Close()
            Throw
        End Try
    End Function ' ExecuteReader

    Private Overloads Shared Function ExecuteReaderNew(ByVal connection As OleDbConnection, _
                                                   ByVal transaction As OleDbTransaction, _
                                                   ByVal commandType As CommandType, _
                                                   ByVal commandText As String, _
                                                   ByVal commandParameters() As OleDbParameter, _
                                                   ByRef cmd As OleDbCommand, _
                                                   ByVal connectionOwnership As OleDBConnectionOwnership) As OleDbDataReader

        If (connection Is Nothing) Then Throw New ArgumentNullException("connection")

        Dim mustCloseConnection As Boolean = False
        ' Create a command and prepare it for execution
        'Dim cmd As New OleDbCommand
        Try
            ' Create a reader
            Dim dataReader As OleDbDataReader

            PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters, mustCloseConnection)

            ' Call ExecuteReader with the appropriate CommandBehavior
            If connectionOwnership = OleDBConnectionOwnership.External Then
                dataReader = cmd.ExecuteReader()
            Else
                dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            End If

            Return dataReader
        Catch
            If (mustCloseConnection) Then connection.Close()
            Throw
        End Try
    End Function ' ExecuteReader

    ' Execute a OleDBCommand (that returns a resultset and takes no parameters) against the database specified in 
    ' the connection string. 
    ' e.g.:  
    ' Dim dr As OleDBDataReader = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders")
    ' Parameters:
    ' -connectionString - a valid connection string for a OleDBConnection 
    ' -commandType - the CommandType (stored procedure, text, etc.) 
    ' -commandText - the stored procedure name or SQL command 
    ' Returns: A OleDBDataReader containing the resultset generated by the command 
    Public Overloads Shared Function ExecuteReader(ByVal connectionString As String, _
                                                   ByVal commandType As CommandType, _
                                                   ByVal commandText As String) As OleDbDataReader
        ' Pass through the call providing null for the set of OleDBParameters
        Return ExecuteReader(connectionString, commandType, commandText, CType(Nothing, OleDbParameter()))
    End Function ' ExecuteReader

    ' Execute a OleDBCommand (that returns a resultset) against the database specified in the connection string 
    ' using the provided parameters.
    ' e.g.:  
    ' Dim dr As OleDBDataReader = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders", new OleDBParameter("@prodid", 24))
    ' Parameters:
    ' -connectionString - a valid connection string for a OleDBConnection 
    ' -commandType - the CommandType (stored procedure, text, etc.) 
    ' -commandText - the stored procedure name or SQL command 
    ' -commandParameters - an array of OleDBParamters used to execute the command 
    ' Returns: A OleDBDataReader containing the resultset generated by the command 
    Public Overloads Shared Function ExecuteReader(ByVal connectionString As String, _
                                                   ByVal commandType As CommandType, _
                                                   ByVal commandText As String, _
                                                   ByVal ParamArray commandParameters() As System.Data.OleDb.OleDbParameter) As OleDbDataReader
        If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")

        ' Create & open a OleDBConnection
        Dim connection As OleDbConnection
        Try
            connection = New OleDbConnection(connectionString)
            connection.Open()
            ' Call the private overload that takes an internally owned connection in place of the connection string
            Return ExecuteReader(connection, CType(Nothing, OleDbTransaction), commandType, commandText, commandParameters, OleDBConnectionOwnership.Internal)
        Catch
            ' If we fail to return the OleDBDatReader, we need to close the connection ourselves
            If Not connection Is Nothing Then connection.Dispose()
            Throw
        End Try
    End Function ' ExecuteReader
   

    ' Execute a OleDBCommand (that returns a resultset and takes no parameters) against the provided OleDBConnection. 
    ' e.g.:  
    ' Dim dr As OleDBDataReader = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders")
    ' Parameters:
    ' -connection - a valid OleDBConnection 
    ' -commandType - the CommandType (stored procedure, text, etc.) 
    ' -commandText - the stored procedure name or SQL command 
    ' Returns: A OleDBDataReader containing the resultset generated by the command 
    Public Overloads Shared Function ExecuteReader(ByVal connection As OleDbConnection, _
                                                   ByVal commandType As CommandType, _
                                                   ByVal commandText As String) As OleDbDataReader

        Return ExecuteReader(connection, commandType, commandText, CType(Nothing, OleDbParameter()))

    End Function ' ExecuteReader

    ' Execute a OleDBCommand (that returns a resultset) against the specified OleDBConnection 
    ' using the provided parameters.
    ' e.g.:  
    ' Dim dr As OleDBDataReader = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders", new OleDBParameter("@prodid", 24))
    ' Parameters:
    ' -connection - a valid OleDBConnection 
    ' -commandType - the CommandType (stored procedure, text, etc.) 
    ' -commandText - the stored procedure name or SQL command 
    ' -commandParameters - an array of OleDBParamters used to execute the command 
    ' Returns: A OleDBDataReader containing the resultset generated by the command 
    Public Overloads Shared Function ExecuteReader(ByVal connection As OleDbConnection, _
                                                   ByVal commandType As CommandType, _
                                                   ByVal commandText As String, _
                                                   ByVal ParamArray commandParameters() As OleDbParameter) As OleDbDataReader
        ' Pass through the call to private overload using a null transaction value
        Return ExecuteReader(connection, CType(Nothing, OleDbTransaction), commandType, commandText, commandParameters, OleDBConnectionOwnership.External)

    End Function ' ExecuteReader

    ' Execute a OleDBCommand (that returns a resultset and takes no parameters) against the provided OleDBTransaction.
    ' e.g.:  
    ' Dim dr As OleDBDataReader = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders")
    ' Parameters:
    ' -transaction - a valid OleDBTransaction  
    ' -commandType - the CommandType (stored procedure, text, etc.) 
    ' -commandText - the stored procedure name or SQL command 
    ' Returns: A OleDBDataReader containing the resultset generated by the command 
    Public Overloads Shared Function ExecuteReader(ByVal transaction As OleDbTransaction, _
                                                   ByVal commandType As CommandType, _
                                                   ByVal commandText As String) As OleDbDataReader
        ' Pass through the call providing null for the set of OleDBParameters
        Return ExecuteReader(transaction, commandType, commandText, CType(Nothing, OleDbParameter()))
    End Function ' ExecuteReader

    ' Execute a OleDBCommand (that returns a resultset) against the specified OleDBTransaction
    ' using the provided parameters.
    ' e.g.:  
    ' Dim dr As OleDBDataReader = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders", new OleDBParameter("@prodid", 24))
    ' Parameters:
    ' -transaction - a valid OleDBTransaction 
    ' -commandType - the CommandType (stored procedure, text, etc.)
    ' -commandText - the stored procedure name or SQL command 
    ' -commandParameters - an array of OleDBParamters used to execute the command 
    ' Returns: A OleDBDataReader containing the resultset generated by the command 
    Public Overloads Shared Function ExecuteReader(ByVal transaction As OleDbTransaction, _
                                                   ByVal commandType As CommandType, _
                                                   ByVal commandText As String, _
                                                   ByVal ParamArray commandParameters() As OleDbParameter) As OleDbDataReader
        If (transaction Is Nothing) Then Throw New ArgumentNullException("transaction")
        If Not (transaction Is Nothing) AndAlso (transaction.Connection Is Nothing) Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
        ' Pass through to private overload, indicating that the connection is owned by the caller
        Return ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandParameters, OleDBConnectionOwnership.External)
    End Function ' ExecuteReader

#End Region

#Region "ExecuteScalar"

    ' Execute a OleDBCommand (that returns a 1x1 resultset and takes no parameters) against the database specified in 
    ' the connection string. 
    ' e.g.:  
    ' Dim orderCount As Integer = CInt(ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount"))
    ' Parameters:
    ' -connectionString - a valid connection string for a OleDBConnection 
    ' -commandType - the CommandType (stored procedure, text, etc.) 
    ' -commandText - the stored procedure name or T-OleDB command 
    ' Returns: An object containing the value in the 1x1 resultset generated by the command
    Public Overloads Shared Function ExecuteScalar(ByVal connectionString As String, _
                                                   ByVal commandType As CommandType, _
                                                   ByVal commandText As String) As Object
        ' Pass through the call providing null for the set of OleDBParameters
        Return ExecuteScalar(connectionString, commandType, commandText, CType(Nothing, OleDbParameter()))
    End Function ' ExecuteScalar

    ' Execute a OleDBCommand (that returns a 1x1 resultset) against the database specified in the connection string 
    ' using the provided parameters.
    ' e.g.:  
    ' Dim orderCount As Integer = Cint(ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount", new OleDBParameter("@prodid", 24)))
    ' Parameters:
    ' -connectionString - a valid connection string for a OleDBConnection 
    ' -commandType - the CommandType (stored procedure, text, etc.) 
    ' -commandText - the stored procedure name or T-OleDB command 
    ' -commandParameters - an array of OleDBParamters used to execute the command 
    ' Returns: An object containing the value in the 1x1 resultset generated by the command 
    Public Overloads Shared Function ExecuteScalar(ByVal connectionString As String, _
                                                   ByVal commandType As CommandType, _
                                                   ByVal commandText As String, _
                                                   ByVal ParamArray commandParameters() As OleDbParameter) As Object
        If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
        ' Create & open a OleDBConnection, and dispose of it after we are done.
        Dim connection As OleDbConnection
        Try
            connection = New OleDbConnection(connectionString)
            connection.Open()

            ' Call the overload that takes a connection in place of the connection string
            Return ExecuteScalar(connection, commandType, commandText, commandParameters)
        Finally
            If Not connection Is Nothing Then connection.Dispose()
        End Try
    End Function ' ExecuteScalar

    ' Execute a OleDBCommand (that returns a 1x1 resultset and takes no parameters) against the provided OleDBConnection. 
    ' e.g.:  
    ' Dim orderCount As Integer = CInt(ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount"))
    ' Parameters:
    ' -connection - a valid OleDBConnection 
    ' -commandType - the CommandType (stored procedure, text, etc.) 
    ' -commandText - the stored procedure name or T-OleDB command 
    ' Returns: An object containing the value in the 1x1 resultset generated by the command 
    Public Overloads Shared Function ExecuteScalar(ByVal connection As OleDbConnection, _
                                                   ByVal commandType As CommandType, _
                                                   ByVal commandText As String) As Object
        ' Pass through the call providing null for the set of OleDBParameters
        Return ExecuteScalar(connection, commandType, commandText, CType(Nothing, OleDbParameter()))
    End Function ' ExecuteScalar

    ' Execute a OleDBCommand (that returns a 1x1 resultset) against the specified OleDBConnection 
    ' using the provided parameters.
    ' e.g.:  
    ' Dim orderCount As Integer = CInt(ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount", new OleDBParameter("@prodid", 24)))
    ' Parameters:
    ' -connection - a valid OleDBConnection 
    ' -commandType - the CommandType (stored procedure, text, etc.) 
    ' -commandText - the stored procedure name or T-OleDB command 
    ' -commandParameters - an array of OleDBParamters used to execute the command 
    ' Returns: An object containing the value in the 1x1 resultset generated by the command 
    Public Overloads Shared Function ExecuteScalar(ByVal connection As OleDbConnection, _
                                                   ByVal commandType As CommandType, _
                                                   ByVal commandText As String, _
                                                   ByVal ParamArray commandParameters() As OleDbParameter) As Object

        If (connection Is Nothing) Then Throw New ArgumentNullException("connection")

        ' Create a command and prepare it for execution
        Dim cmd As New OleDbCommand
        Dim retval As Object
        Dim mustCloseConnection As Boolean = False

        PrepareCommand(cmd, connection, CType(Nothing, OleDbTransaction), commandType, commandText, commandParameters, mustCloseConnection)

        ' Execute the command & return the results
        retval = cmd.ExecuteScalar()

        If (mustCloseConnection) Then connection.Close()

        Return retval

    End Function ' ExecuteScalar

    ' Execute a OleDBCommand (that returns a 1x1 resultset and takes no parameters) against the provided OleDBTransaction.
    ' e.g.:  
    ' Dim orderCount As Integer  = CInt(ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount"))
    ' Parameters:
    ' -transaction - a valid OleDBTransaction 
    ' -commandType - the CommandType (stored procedure, text, etc.) 
    ' -commandText - the stored procedure name or T-OleDB command 
    ' Returns: An object containing the value in the 1x1 resultset generated by the command 
    Public Overloads Shared Function ExecuteScalar(ByVal transaction As OleDbTransaction, _
                                                   ByVal commandType As CommandType, _
                                                   ByVal commandText As String) As Object
        ' Pass through the call providing null for the set of OleDBParameters
        Return ExecuteScalar(transaction, commandType, commandText, CType(Nothing, OleDbParameter()))
    End Function ' ExecuteScalar

    ' Execute a OleDBCommand (that returns a 1x1 resultset) against the specified OleDBTransaction
    ' using the provided parameters.
    ' e.g.:  
    ' Dim orderCount As Integer = CInt(ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount", new OleDBParameter("@prodid", 24)))
    ' Parameters:
    ' -transaction - a valid OleDBTransaction  
    ' -commandType - the CommandType (stored procedure, text, etc.) 
    ' -commandText - the stored procedure name or T-OleDB command 
    ' -commandParameters - an array of OleDBParamters used to execute the command 
    ' Returns: An object containing the value in the 1x1 resultset generated by the command 
    Public Overloads Shared Function ExecuteScalar(ByVal transaction As OleDbTransaction, _
                                                   ByVal commandType As CommandType, _
                                                   ByVal commandText As String, _
                                                   ByVal ParamArray commandParameters() As OleDbParameter) As Object
        If (transaction Is Nothing) Then Throw New ArgumentNullException("transaction")
        If Not (transaction Is Nothing) AndAlso (transaction.Connection Is Nothing) Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")

        ' Create a command and prepare it for execution
        Dim cmd As New OleDbCommand
        Dim retval As Object
        Dim mustCloseConnection As Boolean = False

        PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, mustCloseConnection)

        ' Execute the command & return the results
        retval = cmd.ExecuteScalar()

        Return retval
    End Function ' ExecuteScalar

#End Region

#Region "FillDataset"
    ' Execute a OleDBCommand (that returns a resultset and takes no parameters) against the database specified in 
    ' the connection string. 
    ' e.g.:  
    '   FillDataset (connString, CommandType.StoredProcedure, "GetOrders", ds, new String() {"orders"})
    ' Parameters:    
    ' -connectionString: A valid connection string for a OleDBConnection
    ' -commandType: the CommandType (stored procedure, text, etc.)
    ' -commandText: the stored procedure name or T-OleDB command
    ' -dataSet: A dataset wich will contain the resultset generated by the command
    ' -tableNames: this array will be used to create table mappings allowing the DataTables to be referenced
    '               by a user defined name (probably the actual table name)
    Public Overloads Shared Sub FillDataset(ByVal connectionString As String, ByVal commandType As CommandType, ByVal commandText As String, ByVal dataSet As DataSet, ByVal tableNames() As String)

        If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
        If (dataSet Is Nothing) Then Throw New ArgumentNullException("dataSet")
        ' Create & open a OleDBConnection, and dispose of it after we are done
        Dim connection As OleDbConnection
        Try
            connection = New OleDbConnection(connectionString)
            connection.Open()
            ' Call the overload that takes a connection in place of the connection string
            FillDataset(connection, commandType, commandText, dataSet, tableNames)
        Finally
            If Not connection Is Nothing Then connection.Dispose()
        End Try
    End Sub

    ' Execute a OleDBCommand (that returns a resultset) against the database specified in the connection string 
    ' using the provided parameters.
    ' e.g.:  
    '   FillDataset (connString, CommandType.StoredProcedure, "GetOrders", ds, new String() = {"orders"}, new OleDBParameter("@prodid", 24))
    ' Parameters:    
    ' -connectionString: A valid connection string for a OleDBConnection
    ' -commandType: the CommandType (stored procedure, text, etc.)
    ' -commandText: the stored procedure name or T-OleDB command
    ' -dataSet: A dataset wich will contain the resultset generated by the command
    ' -tableNames: this array will be used to create table mappings allowing the DataTables to be referenced
    '               by a user defined name (probably the actual table name)
    ' -commandParameters: An array of OleDBParamters used to execute the command
    Public Overloads Shared Sub FillDataset(ByVal connectionString As String, ByVal commandType As CommandType, ByVal commandText As String, ByVal dataSet As DataSet, _
        ByVal tableNames() As String, ByVal ParamArray commandParameters() As OleDbParameter)

        If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
        If (dataSet Is Nothing) Then Throw New ArgumentNullException("dataSet")

        ' Create & open a OleDBConnection, and dispose of it after we are done
        Dim connection As OleDbConnection
        Try
            connection = New OleDbConnection(connectionString)

            connection.Open()

            ' Call the overload that takes a connection in place of the connection string
            FillDataset(connection, commandType, commandText, dataSet, tableNames, commandParameters)
        Finally
            If Not connection Is Nothing Then connection.Dispose()
        End Try
    End Sub

    ' Execute a OleDBCommand (that returns a resultset and takes no parameters) against the provided OleDBConnection. 
    ' e.g.:  
    '   FillDataset (conn, CommandType.StoredProcedure, "GetOrders", ds, new String() {"orders"})
    ' Parameters:
    ' -connection: A valid OleDBConnection
    ' -commandType: the CommandType (stored procedure, text, etc.)
    ' -commandText: the stored procedure name or T-OleDB command
    ' -dataSet: A dataset wich will contain the resultset generated by the command
    ' -tableNames: this array will be used to create table mappings allowing the DataTables to be referenced
    ' by a user defined name (probably the actual table name)
    Public Overloads Shared Sub FillDataset(ByVal connection As OleDbConnection, ByVal commandType As CommandType, _
        ByVal commandText As String, ByVal dataSet As DataSet, ByVal tableNames As String())

        FillDataset(connection, commandType, commandText, dataSet, tableNames, Nothing)

    End Sub

    ' Execute a OleDBCommand (that returns a resultset) against the specified OleDBConnection 
    ' using the provided parameters.
    ' e.g.:  
    '   FillDataset (conn, CommandType.StoredProcedure, "GetOrders", ds, new String() {"orders"}, new OleDBParameter("@prodid", 24))
    ' Parameters:
    ' -connection: A valid OleDBConnection
    ' -commandType: the CommandType (stored procedure, text, etc.)
    ' -commandText: the stored procedure name or T-OleDB command
    ' -dataSet: A dataset wich will contain the resultset generated by the command
    ' -tableNames: this array will be used to create table mappings allowing the DataTables to be referenced
    ' by a user defined name (probably the actual table name)
    ' -commandParameters: An array of OleDBParamters used to execute the command
    Public Overloads Shared Sub FillDataset(ByVal connection As OleDbConnection, ByVal commandType As CommandType, _
    ByVal commandText As String, ByVal dataSet As DataSet, ByVal tableNames As String(), _
        ByVal ParamArray commandParameters() As OleDbParameter)

        FillDataset(connection, Nothing, commandType, commandText, dataSet, tableNames, commandParameters)

    End Sub

    ' Execute a OleDBCommand (that returns a resultset and takes no parameters) against the provided OleDBTransaction. 
    ' e.g.:  
    '   FillDataset (trans, CommandType.StoredProcedure, "GetOrders", ds, new string() {"orders"})
    ' Parameters:
    ' -transaction: A valid OleDBTransaction
    ' -commandType: the CommandType (stored procedure, text, etc.)
    ' -commandText: the stored procedure name or T-OleDB command
    ' -dataSet: A dataset wich will contain the resultset generated by the command
    ' -tableNames: this array will be used to create table mappings allowing the DataTables to be referenced
    '             by a user defined name (probably the actual table name)
    Public Overloads Shared Sub FillDataset(ByVal transaction As OleDbTransaction, ByVal commandType As CommandType, _
        ByVal commandText As String, ByVal dataSet As DataSet, ByVal tableNames() As String)

        FillDataset(transaction, commandType, commandText, dataSet, tableNames, Nothing)
    End Sub

    ' Execute a OleDBCommand (that returns a resultset) against the specified OleDBTransaction
    ' using the provided parameters.
    ' e.g.:  
    '   FillDataset(trans, CommandType.StoredProcedure, "GetOrders", ds, new string() {"orders"}, new OleDBParameter("@prodid", 24))
    ' Parameters:
    ' -transaction: A valid OleDBTransaction
    ' -commandType: the CommandType (stored procedure, text, etc.)
    ' -commandText: the stored procedure name or T-OleDB command
    ' -dataSet: A dataset wich will contain the resultset generated by the command
    ' -tableNames: this array will be used to create table mappings allowing the DataTables to be referenced
    ' by a user defined name (probably the actual table name)
    ' -commandParameters: An array of OleDBParamters used to execute the command
    Public Overloads Shared Sub FillDataset(ByVal transaction As OleDbTransaction, ByVal commandType As CommandType, _
        ByVal commandText As String, ByVal dataSet As DataSet, ByVal tableNames() As String, _
        ByVal ParamArray commandParameters() As OleDbParameter)

        If (transaction Is Nothing) Then Throw New ArgumentNullException("transaction")
        If Not (transaction Is Nothing) AndAlso (transaction.Connection Is Nothing) Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
        FillDataset(transaction.Connection, transaction, commandType, commandText, dataSet, tableNames, commandParameters)

    End Sub

    ' Private helper method that execute a OleDBCommand (that returns a resultset) against the specified OleDBTransaction and OleDBConnection
    ' using the provided parameters.
    ' e.g.:  
    '   FillDataset(conn, trans, CommandType.StoredProcedure, "GetOrders", ds, new String() {"orders"}, new OleDBParameter("@prodid", 24))
    ' Parameters:
    ' -connection: A valid OleDBConnection
    ' -transaction: A valid OleDBTransaction
    ' -commandType: the CommandType (stored procedure, text, etc.)
    ' -commandText: the stored procedure name or T-OleDB command
    ' -dataSet: A dataset wich will contain the resultset generated by the command
    ' -tableNames: this array will be used to create table mappings allowing the DataTables to be referenced
    '             by a user defined name (probably the actual table name)
    ' -commandParameters: An array of OleDBParamters used to execute the command
    Private Overloads Shared Sub FillDataset(ByVal connection As OleDbConnection, ByVal transaction As OleDbTransaction, ByVal commandType As CommandType, _
        ByVal commandText As String, ByVal dataSet As DataSet, ByVal tableNames() As String, _
        ByVal ParamArray commandParameters() As OleDbParameter)

        If (connection Is Nothing) Then Throw New ArgumentNullException("connection")
        If (dataSet Is Nothing) Then Throw New ArgumentNullException("dataSet")

        ' Create a command and prepare it for execution
        Dim command As New OleDbCommand

        Dim mustCloseConnection As Boolean = False
        PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters, mustCloseConnection)

        ' Create the DataAdapter & DataSet
        Dim dataAdapter As OleDbDataAdapter = New OleDbDataAdapter(command)

        Try
            ' Add the table mappings specified by the user
            If Not tableNames Is Nothing AndAlso tableNames.Length > 0 Then

                Dim tableName As String = "Table"
                Dim index As Integer

                For index = 0 To tableNames.Length - 1
                    If (tableNames(index) Is Nothing OrElse tableNames(index).Length = 0) Then Throw New ArgumentException("The tableNames parameter must contain a list of tables, a value was provided as null or empty string.", "tableNames")
                    dataAdapter.TableMappings.Add(tableName, tableNames(index))
                    tableName = tableName & (index + 1).ToString()
                Next
            End If

            ' Fill the DataSet using default values for DataTable names, etc
            dataAdapter.Fill(dataSet)

        Finally
            If (Not dataAdapter Is Nothing) Then dataAdapter.Dispose()
        End Try

        If (mustCloseConnection) Then connection.Close()

    End Sub
#End Region

#Region "UpdateDataset"
    ' Executes the respective command for each inserted, updated, or deleted row in the DataSet.
    ' e.g.:  
    '   UpdateDataset(conn, insertCommand, deleteCommand, updateCommand, dataSet, "Order")
    ' Parameters:
    ' -insertCommand: A valid transact-OleDB statement or stored procedure to insert new records into the data source
    ' -deleteCommand: A valid transact-OleDB statement or stored procedure to delete records from the data source
    ' -updateCommand: A valid transact-OleDB statement or stored procedure used to update records in the data source
    ' -dataSet: the DataSet used to update the data source
    ' -tableName: the DataTable used to update the data source
    Public Overloads Shared Sub UpdateDataset(ByVal insertCommand As OleDbCommand, ByVal deleteCommand As OleDbCommand, ByVal updateCommand As OleDbCommand, ByVal dataSet As DataSet, ByVal tableName As String)

        If (insertCommand Is Nothing) Then Throw New ArgumentNullException("insertCommand")
        If (deleteCommand Is Nothing) Then Throw New ArgumentNullException("deleteCommand")
        If (updateCommand Is Nothing) Then Throw New ArgumentNullException("updateCommand")
        If (dataSet Is Nothing) Then Throw New ArgumentNullException("dataSet")
        If (tableName Is Nothing OrElse tableName.Length = 0) Then Throw New ArgumentNullException("tableName")

        ' Create a OleDBDataAdapter, and dispose of it after we are done
        Dim dataAdapter As New OleDbDataAdapter
        Try
            ' Set the data adapter commands
            dataAdapter.UpdateCommand = updateCommand
            dataAdapter.InsertCommand = insertCommand
            dataAdapter.DeleteCommand = deleteCommand

            ' Update the dataset changes in the data source
            dataAdapter.Update(dataSet, tableName)

            ' Commit all the changes made to the DataSet
            dataSet.AcceptChanges()
        Finally
            If (Not dataAdapter Is Nothing) Then dataAdapter.Dispose()
        End Try
    End Sub
#End Region

End Class ' OleDBHelper

' OleDBHelperParameterCache provides functions to leverage a static cache of procedure parameters, and the
' ability to discover parameters for stored procedures at run-time.
Public NotInheritable Class OleDBHelperParameterCache

#Region "private methods, variables, and constructors"


    ' Since this class provides only static methods, make the default constructor private to prevent 
    ' instances from being created with "new OleDBHelperParameterCache()".
    Private Sub New()
    End Sub ' New 

    Private Shared paramCache As Hashtable = Hashtable.Synchronized(New Hashtable)

    ' Deep copy of cached OleDBParameter array
    Private Shared Function CloneParameters(ByVal originalParameters() As OleDbParameter) As OleDbParameter()

        Dim i As Integer
        Dim j As Integer = originalParameters.Length - 1
        Dim clonedParameters(j) As OleDbParameter

        For i = 0 To j
            clonedParameters(i) = CType(CType(originalParameters(i), ICloneable).Clone, OleDbParameter)
        Next

        Return clonedParameters
    End Function ' CloneParameters

#End Region

#Region "caching functions"

    ' add parameter array to the cache
    ' Parameters
    ' -connectionString - a valid connection string for a OleDBConnection 
    ' -commandText - the stored procedure name or T-OleDB command 
    ' -commandParameters - an array of OleDBParamters to be cached 
    Public Shared Sub CacheParameterSet(ByVal connectionString As String, _
                                        ByVal commandText As String, _
                                        ByVal ParamArray commandParameters() As OleDbParameter)
        If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
        If (commandText Is Nothing OrElse commandText.Length = 0) Then Throw New ArgumentNullException("commandText")

        Dim hashKey As String = connectionString + ":" + commandText

        paramCache(hashKey) = commandParameters
    End Sub ' CacheParameterSet

    ' retrieve a parameter array from the cache
    ' Parameters:
    ' -connectionString - a valid connection string for a OleDBConnection 
    ' -commandText - the stored procedure name or T-OleDB command 
    ' Returns: An array of OleDBParamters 
    Public Shared Function GetCachedParameterSet(ByVal connectionString As String, ByVal commandText As String) As OleDbParameter()
        If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
        If (commandText Is Nothing OrElse commandText.Length = 0) Then Throw New ArgumentNullException("commandText")

        Dim hashKey As String = connectionString + ":" + commandText
        Dim cachedParameters As OleDbParameter() = CType(paramCache(hashKey), OleDbParameter())

        If cachedParameters Is Nothing Then
            Return Nothing
        Else
            Return CloneParameters(cachedParameters)
        End If
    End Function ' GetCachedParameterSet

#End Region

End Class ' OleDBHelperParameterCache 