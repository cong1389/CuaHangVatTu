Imports System.Data
Imports Microsoft.VisualBasic

Namespace adv.Business
    Public Class MainternanceAddress
        Private mNo_ As String = ""
        Private mMenuNo_ As String = ""
        Private mDescription As String = ""
        Private mContent As String = ""
        Property No_ As String
            Get
                Return mNo_
            End Get
            Set(value As String)
                mNo_ = value
            End Set
        End Property
        Property MenuNo_ As String
            Get
                Return mMenuNo_
            End Get
            Set(value As String)
                mMenuNo_ = value
            End Set
        End Property
        Property Description As String
            Get
                Return mDescription
            End Get
            Set(value As String)
                mDescription = value
            End Set
        End Property
        Property Content As String
            Get
                Return mContent
            End Get
            Set(value As String)
                mContent = value
            End Set
        End Property
        Public Function Load(ByVal sNo As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Mainternance Address] WHERE No_ = '" & sNo & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mMenuNo_ = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mDescription = GetValue(SqlReader, 2, typeOfColumn.GetString)
                    mContent = GetValue(SqlReader, 3, typeOfColumn.GetString)
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
            Dim arParams() As IDataParameter = New IDataParameter(3) {}
            Try
                arParams(0) = DBHelper.createParameter("@No_", SqlDbType.NVarChar, ParameterDirection.Input, mNo_)
                arParams(1) = DBHelper.createParameter("@MenuNo_", SqlDbType.NVarChar, ParameterDirection.Input, mMenuNo_)
                arParams(2) = DBHelper.createParameter("@Desctiption", SqlDbType.NVarChar, ParameterDirection.Input, mDescription)
                arParams(3) = DBHelper.createParameter("@Content", SqlDbType.NVarChar, ParameterDirection.Input, mContent)
                
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "Sp_MainternanceAddress", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
    End Class
End Namespace