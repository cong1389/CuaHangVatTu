Imports Microsoft.VisualBasic
Imports System.Data

Namespace adv.Business
    Public Class Page
#Region "Khai báo"
        Private mNo_ As String = ""
        Private mName As String = ""
        Private mWebMobile As Integer = 0
        Private mLinkURL As String = ""
        Private mCssClass As String = ""
        Private mBackgroundColor As String = ""
        Private mButtonColor As String = ""

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
        Property WebMobile() As Integer
            Get
                Return mWebMobile
            End Get
            Set(ByVal value As Integer)
                mWebMobile = value
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
        Property CssClass() As String
            Get
                Return mCssClass
            End Get
            Set(ByVal value As String)
                mCssClass = value
            End Set
        End Property
        Property BackgroundColor() As String
            Get
                Return mBackgroundColor
            End Get
            Set(ByVal value As String)
                mBackgroundColor = value
            End Set
        End Property
        Property ButtonColor As String
            Get
                Return mButtonColor
            End Get
            Set(ByVal value As String)
                mButtonColor = value
            End Set
        End Property
#End Region
#Region "Function"
        Public Function Load(ByVal sNo As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Page] WHERE No_ = '" & sNo & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mName = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mWebMobile = GetValue(SqlReader, 2, typeOfColumn.GetInt32)
                    mLinkURL = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mCssClass = GetValue(SqlReader, 4, typeOfColumn.GetString)
                    mBackgroundColor = GetValue(SqlReader, 5, typeOfColumn.GetString)
                    mButtonColor = GetValue(SqlReader, 6, typeOfColumn.GetString)
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

        Public Function LoadByLink(ByVal sLink As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Page] WHERE [Link URL] = '" & sLink & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mName = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mWebMobile = GetValue(SqlReader, 2, typeOfColumn.GetInt32)
                    mLinkURL = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mCssClass = GetValue(SqlReader, 4, typeOfColumn.GetString)
                    mBackgroundColor = GetValue(SqlReader, 5, typeOfColumn.GetString)
                    mButtonColor = GetValue(SqlReader, 6, typeOfColumn.GetString)
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
            Dim arParams() As IDataParameter = New IDataParameter(6) {}
            Try
                arParams(0) = DBHelper.createParameter("@No_", SqlDbType.NVarChar, ParameterDirection.Input, mNo_)
                arParams(1) = DBHelper.createParameter("@Name", SqlDbType.NVarChar, ParameterDirection.Input, mName)
                arParams(2) = DBHelper.createParameter("@WebMobile", SqlDbType.Int, ParameterDirection.Input, mWebMobile)
                arParams(3) = DBHelper.createParameter("@LinkURL", SqlDbType.NVarChar, ParameterDirection.Input, mLinkURL)
                arParams(4) = DBHelper.createParameter("@CssClass", SqlDbType.NVarChar, ParameterDirection.Input, mCssClass)
                arParams(5) = DBHelper.createParameter("@BackgroundColor", SqlDbType.NVarChar, ParameterDirection.Input, mBackgroundColor)
                arParams(6) = DBHelper.createParameter("@ButtonColor", SqlDbType.NVarChar, ParameterDirection.Input, mButtonColor)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_Page", arParams)
                Return True
            Catch ex As Exception

                Return False
            End Try
        End Function

        Public Function UpdateLasVisit(ByVal sGuestNo As String, ByVal sPageNo As String) As Boolean
            Dim arParams() As IDataParameter = New IDataParameter(1) {}
            Try
                arParams(0) = DBHelper.createParameter("@GuestNo_", SqlDbType.NVarChar, ParameterDirection.Input, sGuestNo)
                arParams(1) = DBHelper.createParameter("@PageNo", SqlDbType.NVarChar, ParameterDirection.Input, sPageNo)

                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "W_LastVisit", arParams)
                Return True
            Catch ex As Exception

                Return False
            End Try
        End Function

        Function GetLastPage(ByVal sGuestNo As String) As String
            Dim SQL As String
            SQL = "SELECT [Page No_] FROM [Last Visit] WHERE [Guest No_] = '" & sGuestNo & "'"
            Return ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), "").ToString.Trim
        End Function

        Function GetTopSeller(ByVal sPageNo As String, ByVal sDivisionNo As String) As DataTable
            Dim arParams() As IDataParameter = New IDataParameter(1) {}
            Try
                arParams(0) = DBHelper.createParameter("@PageNo", SqlDbType.NVarChar, ParameterDirection.Input, sPageNo)
                arParams(1) = DBHelper.createParameter("@DivisionNo", SqlDbType.NVarChar, ParameterDirection.Input, sDivisionNo)
                Return DBHelper.ExecuteDataset(GetConnectString, CommandType.StoredProcedure, "W_GetTopSeller", arParams).Tables(0)

            Catch ex As Exception
                Return Nothing
            End Try
        End Function

#End Region

    End Class

End Namespace