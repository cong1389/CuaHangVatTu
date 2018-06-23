Imports Microsoft.VisualBasic
Imports System.Data
Namespace adv.Business
    Public Class MenuItem
#Region "Khai báo"
        Private mNo_ As String = ""
        Private mName As String = ""
        Private mMenuType As Integer = 0
        Private mMenuOrder As Integer = 0
        Private mParentNo_ As String = ""
        Private mLinkDisplay As String = ""
        Private mPublished As Integer = 0
        Private mImagesURL As String = ""
        Private mImagesThumURL As String = ""
        Private mBackgroundColor As String = ""
        Private mUsingFor As Integer = 0
        Private mCssFull As String = ""
        Private mCssFullActive As String = ""
        Private mCssThumb As String = ""
        Private mCssThumbActive As String = ""
        Private mImagesThumbURLActive As String = ""
        Private mParentLink As String = ""
        Private mMetaKeywords As String = ""
        Private mWebMobile As Integer = 0
        Private mPageNo_ As String = ""
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
        Property MenuType() As Integer
            Get
                Return mMenuType
            End Get
            Set(ByVal value As Integer)
                mMenuType = value
            End Set
        End Property
        Property MenuOrder() As Integer
            Get
                Return mMenuOrder
            End Get
            Set(ByVal value As Integer)
                mMenuOrder = value
            End Set
        End Property
        Property ParentNo_() As String
            Get
                Return mParentNo_
            End Get
            Set(ByVal value As String)
                mParentNo_ = value
            End Set
        End Property
        Property LinkDisplay() As String
            Get
                Return mLinkDisplay
            End Get
            Set(ByVal value As String)
                mLinkDisplay = value
            End Set
        End Property
        Property Published() As Integer
            Get
                Return mPublished
            End Get
            Set(ByVal value As Integer)
                mPublished = value
            End Set
        End Property
        Property ImagesURL() As String
            Get
                Return mImagesURL
            End Get
            Set(ByVal value As String)
                mImagesURL = value
            End Set
        End Property
        Property ImagesThumURL() As String
            Get
                Return mImagesThumURL
            End Get
            Set(ByVal value As String)
                mImagesThumURL = value
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
        Property UsingFor() As Integer
            Get
                Return mUsingFor
            End Get
            Set(ByVal value As Integer)
                mUsingFor = value
            End Set
        End Property
        Property CssFull() As String
            Get
                Return mCssFull
            End Get
            Set(ByVal value As String)
                mCssFull = value
            End Set
        End Property
        Property CssFullActive() As String
            Get
                Return mCssFullActive
            End Get
            Set(ByVal value As String)
                mCssFullActive = value
            End Set
        End Property
        Property CssThumb() As String
            Get
                Return mCssThumb
            End Get
            Set(ByVal value As String)
                mCssThumb = value
            End Set
        End Property
        Property CssThumbActive() As String
            Get
                Return mCssThumbActive
            End Get
            Set(ByVal value As String)
                mCssThumbActive = value
            End Set
        End Property
        Property ImagesThumbURLActive() As String
            Get
                Return mImagesThumbURLActive
            End Get
            Set(ByVal value As String)
                mImagesThumbURLActive = value
            End Set
        End Property
        Property ParentLink() As String
            Get
                Return mParentLink
            End Get
            Set(ByVal value As String)
                mParentLink = value
            End Set
        End Property
        Property MetaKeywords() As String
            Get
                Return mMetaKeywords
            End Get
            Set(ByVal value As String)
                mMetaKeywords = value
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
        Property PageNo_() As String
            Get
                Return mPageNo_
            End Get
            Set(ByVal value As String)
                mPageNo_ = value
            End Set
        End Property

#End Region
#Region "Function"
        Public Function Load(ByVal sNo_ As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Good Menu] WHERE No_ = '" & sNo_ & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mName = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mMenuType = GetValue(SqlReader, 2, typeOfColumn.GetInt32)
                    mMenuOrder = GetValue(SqlReader, 3, typeOfColumn.GetInt32)
                    mParentNo_ = GetValue(SqlReader, 4, typeOfColumn.GetString)
                    mLinkDisplay = GetValue(SqlReader, 5, typeOfColumn.GetString)
                    mPublished = GetValue(SqlReader, 6, typeOfColumn.GetInt32)
                    mImagesURL = GetValue(SqlReader, 7, typeOfColumn.GetString)
                    mImagesThumURL = GetValue(SqlReader, 8, typeOfColumn.GetString)
                    mBackgroundColor = GetValue(SqlReader, 9, typeOfColumn.GetString)
                    mUsingFor = GetValue(SqlReader, 10, typeOfColumn.GetInt32)
                    mCssFull = GetValue(SqlReader, 11, typeOfColumn.GetString)
                    mCssFullActive = GetValue(SqlReader, 12, typeOfColumn.GetString)
                    mCssThumb = GetValue(SqlReader, 13, typeOfColumn.GetString)
                    mCssThumbActive = GetValue(SqlReader, 14, typeOfColumn.GetString)
                    mImagesThumbURLActive = GetValue(SqlReader, 15, typeOfColumn.GetString)
                    mParentLink = GetValue(SqlReader, 16, typeOfColumn.GetString)
                    mMetaKeywords = GetValue(SqlReader, 17, typeOfColumn.GetString)
                    mWebMobile = GetValue(SqlReader, 18, typeOfColumn.GetInt32)
                    mPageNo_ = GetValue(SqlReader, 19, typeOfColumn.GetString)
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
                StrSQL = " SELECT * FROM [Good Menu] WHERE [Link Display] = '" & sLink & "' "

                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mName = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mMenuType = GetValue(SqlReader, 2, typeOfColumn.GetInt32)
                    mMenuOrder = GetValue(SqlReader, 3, typeOfColumn.GetInt32)
                    mParentNo_ = GetValue(SqlReader, 4, typeOfColumn.GetString)
                    mLinkDisplay = GetValue(SqlReader, 5, typeOfColumn.GetString)
                    mPublished = GetValue(SqlReader, 6, typeOfColumn.GetInt32)
                    mImagesURL = GetValue(SqlReader, 7, typeOfColumn.GetString)
                    mImagesThumURL = GetValue(SqlReader, 8, typeOfColumn.GetString)
                    mBackgroundColor = GetValue(SqlReader, 9, typeOfColumn.GetString)
                    mUsingFor = GetValue(SqlReader, 10, typeOfColumn.GetInt32)
                    mCssFull = GetValue(SqlReader, 11, typeOfColumn.GetString)
                    mCssFullActive = GetValue(SqlReader, 12, typeOfColumn.GetString)
                    mCssThumb = GetValue(SqlReader, 13, typeOfColumn.GetString)
                    mCssThumbActive = GetValue(SqlReader, 14, typeOfColumn.GetString)
                    mImagesThumbURLActive = GetValue(SqlReader, 15, typeOfColumn.GetString)
                    mParentLink = GetValue(SqlReader, 16, typeOfColumn.GetString)
                    mMetaKeywords = GetValue(SqlReader, 17, typeOfColumn.GetString)
                    mWebMobile = GetValue(SqlReader, 18, typeOfColumn.GetInt32)
                    mPageNo_ = GetValue(SqlReader, 19, typeOfColumn.GetString)
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
            Dim arParams() As IDataParameter = New IDataParameter(19) {}
            Try
                arParams(0) = DBHelper.createParameter("@No_", SqlDbType.NVarChar, ParameterDirection.Input, mNo_)
                arParams(1) = DBHelper.createParameter("@Name", SqlDbType.NVarChar, ParameterDirection.Input, mName)
                arParams(2) = DBHelper.createParameter("@MenuType", SqlDbType.Int, ParameterDirection.Input, mMenuType)
                arParams(3) = DBHelper.createParameter("@MenuOrder", SqlDbType.Int, ParameterDirection.Input, mMenuOrder)
                arParams(4) = DBHelper.createParameter("@ParentNo_", SqlDbType.NVarChar, ParameterDirection.Input, mParentNo_)
                arParams(5) = DBHelper.createParameter("@LinkDisplay", SqlDbType.NVarChar, ParameterDirection.Input, mLinkDisplay)
                arParams(6) = DBHelper.createParameter("@Published", SqlDbType.Int, ParameterDirection.Input, mPublished)
                arParams(7) = DBHelper.createParameter("@ImagesURL", SqlDbType.NVarChar, ParameterDirection.Input, mImagesURL)
                arParams(8) = DBHelper.createParameter("@ImagesThumURL", SqlDbType.NVarChar, ParameterDirection.Input, mImagesThumURL)
                arParams(9) = DBHelper.createParameter("@BackgroundColor", SqlDbType.NVarChar, ParameterDirection.Input, mBackgroundColor)
                arParams(10) = DBHelper.createParameter("@UsingFor", SqlDbType.Int, ParameterDirection.Input, mUsingFor)
                arParams(11) = DBHelper.createParameter("@CssFull", SqlDbType.NVarChar, ParameterDirection.Input, mCssFull)
                arParams(12) = DBHelper.createParameter("@CssFullActive", SqlDbType.NVarChar, ParameterDirection.Input, mCssFullActive)
                arParams(13) = DBHelper.createParameter("@CssThumb", SqlDbType.NVarChar, ParameterDirection.Input, mCssThumb)
                arParams(14) = DBHelper.createParameter("@CssThumbActive", SqlDbType.NVarChar, ParameterDirection.Input, mCssThumbActive)
                arParams(15) = DBHelper.createParameter("@ImagesThumbURLActive", SqlDbType.NVarChar, ParameterDirection.Input, mImagesThumbURLActive)
                arParams(16) = DBHelper.createParameter("@ParentLink", SqlDbType.NVarChar, ParameterDirection.Input, mParentLink)
                arParams(17) = DBHelper.createParameter("@MetaKeywords", SqlDbType.NVarChar, ParameterDirection.Input, mMetaKeywords)
                arParams(18) = DBHelper.createParameter("@WebMobile", SqlDbType.Int, ParameterDirection.Input, mWebMobile)
                arParams(19) = DBHelper.createParameter("@PageNo_", SqlDbType.NVarChar, ParameterDirection.Input, mPageNo_)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_GoodMenu", arParams)
                Return True
            Catch ex As Exception

                Return False
            End Try
        End Function

        Public Sub SetBlank()
            mNo_ = ""
            mName = ""
            mMenuType = 0
            mMenuOrder = 0
            mParentNo_ = ""
            mLinkDisplay = ""
            mPublished = 0
            mImagesURL = ""
            mImagesThumURL = ""
            mPageNo_ = ""
        End Sub

        Function GetFullLink(ByVal sCatNo As String) As String
            Dim sUrl As String
            Dim sValue As String = ""
            sUrl = GetUrl()
            Dim SQL As String
            SQL = " SELECT [Parent Link] + '/' + [Link Display] + '/' FROM [Good Menu] WHERE No_ = (SELECT [Parent No_] FROM [Good Menu] WHERE No_ = '" & sCatNo & "') "
            sValue = DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL)
            Return sUrl & sValue
        End Function

        Function GetPareNo(ByVal sMenuNo As String) As String
            Dim SQL As String
            SQL = " SELECT [Parent No_] FROM [Good Menu] WHERE No_ = '" & sMenuNo & "' "
            Return ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), "").ToString.Trim
        End Function

#End Region
    End Class

End Namespace
