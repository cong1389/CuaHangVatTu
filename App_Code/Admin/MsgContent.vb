Imports Microsoft.VisualBasic
Imports System.Data

Namespace adv.Business
    Public Class MsgContent
#Region "Khai báo"
        Private mNo_ As String = ""
        Private mName As String = ""
        Private mContent As String = ""
        Private mLinkURL As String = ""

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
        Property Content() As String
            Get
                Return mContent
            End Get
            Set(ByVal value As String)
                mContent = value
            End Set
        End Property
        Property LinkURL() As String
            Get
                Return mLinkURL
            End Get
            Set(ByVal value As String)
                mLinkURL = value
            End Set
        End Property

#End Region
#Region "Function"
        Public Function Load(ByVal sNo As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Msg Content] WHERE No_ = '" & sNo & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mName = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mContent = GetValue(SqlReader, 2, typeOfColumn.GetString)
                    mLinkURL = GetValue(SqlReader, 3, typeOfColumn.GetString)
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
                arParams(1) = DBHelper.createParameter("@Name", SqlDbType.NVarChar, ParameterDirection.Input, mName)
                arParams(2) = DBHelper.createParameter("@Content", SqlDbType.NVarChar, ParameterDirection.Input, mContent)
                arParams(3) = DBHelper.createParameter("@LinkURL", SqlDbType.NVarChar, ParameterDirection.Input, mLinkURL)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_MsgContent", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
#End Region

    End Class
End Namespace