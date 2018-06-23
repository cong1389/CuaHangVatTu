Imports Microsoft.VisualBasic
Imports System.Data

Namespace adv.Business
    Public Class ContentType
#Region "Khai báo"
        Private mNo_ As Integer = 0
        Private mName As String = ""
        Private mLinkMenu As String = ""

        Property No_() As Integer
            Get
                Return mNo_
            End Get
            Set(ByVal value As Integer)
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
        Property LinkMenu() As String
            Get
                Return mLinkMenu
            End Get
            Set(ByVal value As String)
                mLinkMenu = value
            End Set
        End Property

#End Region
#Region "Function"
        Public Function Load(ByVal iNo As Integer) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Content Type] WHERE No_ = " & iNo & ""
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = GetValue(SqlReader, 0, typeOfColumn.GetInt32)
                    mName = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mLinkMenu = GetValue(SqlReader, 2, typeOfColumn.GetString)
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

        Public Function LoadByLinkMenu(ByVal sLink As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Content Type] WHERE [Link Menu] = '" & sLink & "'"
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = GetValue(SqlReader, 0, typeOfColumn.GetInt32)
                    mName = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mLinkMenu = GetValue(SqlReader, 2, typeOfColumn.GetString)
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
            Dim arParams() As IDataParameter = New IDataParameter(2) {}
            Try
                arParams(0) = DBHelper.createParameter("@No_", SqlDbType.Int, ParameterDirection.Input, mNo_)
                arParams(1) = DBHelper.createParameter("@Name", SqlDbType.NVarChar, ParameterDirection.Input, mName)
                arParams(2) = DBHelper.createParameter("@LinkMenu", SqlDbType.NVarChar, ParameterDirection.Input, mLinkMenu)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_Content Type", arParams)
                Return True
            Catch ex As Exception

                Return False
            End Try
        End Function
#End Region

    End Class
End Namespace