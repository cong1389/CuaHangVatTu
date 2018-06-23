Imports System.Data
Imports Microsoft.VisualBasic

Namespace adv.Business
    Public Class FeatureGroup
#Region "Khai báo"
        Private mNo_ As String = ""
        Private mName As String = ""
        Private mType As Integer = 0
        Private mDescriptions As String = ""

        Property No_() As String
            Get
                Return mNo_
            End Get
            Set(ByVal value As String)
                mNo_ = value
            End Set
        End Property
        Property Name() As String
            Get
                Return mName
            End Get
            Set(ByVal value As String)
                mName = value
            End Set
        End Property
        Property Type() As Integer
            Get
                Return mType
            End Get
            Set(ByVal value As Integer)
                mType = value
            End Set
        End Property
        Property Descriptions() As String
            Get
                Return mDescriptions
            End Get
            Set(ByVal value As String)
                mDescriptions = value
            End Set
        End Property

#End Region
#Region "Function"
        Public Function Load(ByVal sNo_ As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Feature Group] WHERE No_ = '" & sNo_ & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = adv.Business.GetValue(SqlReader, 0, adv.Business.typeOfColumn.GetString)
                    mName = adv.Business.GetValue(SqlReader, 1, adv.Business.typeOfColumn.GetString)
                    mType = adv.Business.GetValue(SqlReader, 2, adv.Business.typeOfColumn.GetInt32)
                    mDescriptions = adv.Business.GetValue(SqlReader, 3, adv.Business.typeOfColumn.GetString)
                    SqlReader.Close()
                    Return True
                Else
                    SqlReader.Close()
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Function Save() As Boolean
            Try
                Dim SQL As String
                SQL = "  INSERT INTO [Feature Group] ([No_],[Name],[Type],[Descriptions]) " & _
                    " VALUES ('" & mNo_ & "','" & mName & "', " & mType & " , '" & mDescriptions & "')"
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Function Update(ByVal sOldNo_ As String) As Boolean
            Try
                Dim SQL As String
                SQL = "DELETE FROM [Feature Group] WHERE No_ = '" & sOldNo_ & "'"
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
                SQL = "  INSERT INTO [Feature Group] ([No_],[Name],[Type],[Descriptions]) " & _
                    " VALUES ('" & mNo_ & "',N'" & mName & "', " & mType & " , N'" & mDescriptions & "')"
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Sub SetBlank()
            mNo_ = ""
            mName = ""
            mType = 0
            mDescriptions = ""
        End Sub
        Function CheckExistsNo(ByVal sNoCheck As String) As Boolean
            Dim SQL As String
            Dim sValue As String
            SQL = " SELECT No_ FROM [Feature Group] WHERE No_ = '" & sNoCheck.Trim & "'"
            sValue = ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), "").ToString.Trim
            Return sValue.Trim = ""
        End Function

#End Region

    End Class
End Namespace