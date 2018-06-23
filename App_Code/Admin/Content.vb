Imports Microsoft.VisualBasic
Imports System.Data

Namespace adv.Business
    Public Class Content
#Region "Khai báo"
        Private mNo_ As String = ""
        Private mCategoryNo_ As String = ""
        Private mGroupNo_ As String = ""
        Private mTitle As String = ""
        Private mDateCreate As String = ""
        Private mPublished As Integer = 0
        Private mContent As String = ""
        Private mClicked As Integer = 0
        Private mMenuDivisionNo_ As String = ""
        Private mMenuCategoryNo_ As String = ""
        Private mMenuGroupNo_ As String = ""
        Private mItemNo_ As String = ""
        Private mContentType As Integer = 0
        Private mType As Integer = 0
        Private mShowDefault As Integer = 0
        Private mUserCreate As String = ""
        Private mLinkMenu As String = ""
        Private mOrderPosition As Integer = 0
        Private mSummary As String = ""
        Private mImagesURL As String = ""
        Private mHotContent As Integer = 0
        Private mTimeCreate As String = ""
        Private mMetaKeywords As String = ""
        Property No_() As String
            Get
                Return mNo_
            End Get
            Set(ByVal value As String)
                mNo_ = value
            End Set
        End Property
        Property CategoryNo_() As String
            Get
                Return mCategoryNo_
            End Get
            Set(ByVal value As String)
                mCategoryNo_ = value
            End Set
        End Property
        Property GroupNo_() As String
            Get
                Return mGroupNo_
            End Get
            Set(ByVal value As String)
                mGroupNo_ = value
            End Set
        End Property
        Property Title() As String
            Get
                Return mTitle
            End Get
            Set(ByVal value As String)
                mTitle = value
            End Set
        End Property
        Property DateCreate() As String
            Get
                Return mDateCreate
            End Get
            Set(ByVal value As String)
                mDateCreate = value
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
        Property Content() As String
            Get
                Return mContent
            End Get
            Set(ByVal value As String)
                mContent = value
            End Set
        End Property
        Property Clicked() As Integer
            Get
                Return mClicked
            End Get
            Set(ByVal value As Integer)
                mClicked = value
            End Set
        End Property
        Property MenuDivisionNo_() As String
            Get
                Return mMenuDivisionNo_
            End Get
            Set(ByVal value As String)
                mMenuDivisionNo_ = value
            End Set
        End Property
        Property MenuCategoryNo_() As String
            Get
                Return mMenuCategoryNo_
            End Get
            Set(ByVal value As String)
                mMenuCategoryNo_ = value
            End Set
        End Property
        Property MenuGroupNo_() As String
            Get
                Return mMenuGroupNo_
            End Get
            Set(ByVal value As String)
                mMenuGroupNo_ = value
            End Set
        End Property
        Property ItemNo_() As String
            Get
                Return mItemNo_
            End Get
            Set(ByVal value As String)
                mItemNo_ = value
            End Set
        End Property
        Property ContentType() As Integer
            Get
                Return mContentType
            End Get
            Set(ByVal value As Integer)
                mContentType = value
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
        Property ShowDefault() As Integer
            Get
                Return mShowDefault
            End Get
            Set(ByVal value As Integer)
                mShowDefault = value
            End Set
        End Property
        Property UserCreate() As String
            Get
                Return mUserCreate
            End Get
            Set(ByVal value As String)
                mUserCreate = value
            End Set
        End Property
        Property LinkMenu As String
            Get
                Return mLinkMenu
            End Get
            Set(ByVal value As String)
                mLinkMenu = value
            End Set
        End Property
        Property OrderPosition As Integer
            Get
                Return mOrderPosition
            End Get
            Set(ByVal value As Integer)
                mOrderPosition = value
            End Set
        End Property
        Property Summary() As String
            Get
                Return mSummary
            End Get
            Set(ByVal value As String)
                mSummary = value
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
        Property HotContent() As Integer
            Get
                Return mHotContent
            End Get
            Set(ByVal value As Integer)
                mHotContent = value
            End Set
        End Property
        Property TimeCreate As String
            Get
                Return mTimeCreate
            End Get
            Set(ByVal value As String)
                mTimeCreate = value
            End Set
        End Property
        Property MetaKeywords As String
            Get
                Return mMetaKeywords
            End Get
            Set(ByVal value As String)
                mMetaKeywords = value
            End Set
        End Property
#End Region
#Region "Function"

        Public Function LoadSumary(ByVal sContentNo As String) As Boolean
            Dim SQL As String
            Dim SqlReader As IDataReader
            Try
                SQL = "SELECT No_, Title, [Date Create], [Time Create], Summary, [Images URL], [Link Menu] FROM Content WHERE No_ = '" & sContentNo & "'"
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, SQL)
                If SqlReader.Read() Then
                    mNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mTitle = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mDateCreate = GetValue(SqlReader, 2, typeOfColumn.GetString)
                    mTimeCreate = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mSummary = GetValue(SqlReader, 4, typeOfColumn.GetString)
                    mImagesURL = GetValue(SqlReader, 5, typeOfColumn.GetString)
                    mLinkMenu = GetValue(SqlReader, 6, typeOfColumn.GetString)
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

        Public Function Load(ByVal sContentNo As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Content] WHERE No_ = '" & sContentNo & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mCategoryNo_ = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mGroupNo_ = GetValue(SqlReader, 2, typeOfColumn.GetString)
                    mTitle = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mDateCreate = GetValue(SqlReader, 4, typeOfColumn.GetString)
                    mPublished = GetValue(SqlReader, 5, typeOfColumn.GetInt32)
                    mContent = GetValue(SqlReader, 6, typeOfColumn.GetString)
                    mClicked = GetValue(SqlReader, 7, typeOfColumn.GetInt32)
                    mMenuDivisionNo_ = GetValue(SqlReader, 8, typeOfColumn.GetString)
                    mMenuCategoryNo_ = GetValue(SqlReader, 9, typeOfColumn.GetString)
                    mMenuGroupNo_ = GetValue(SqlReader, 10, typeOfColumn.GetString)
                    mItemNo_ = GetValue(SqlReader, 11, typeOfColumn.GetString)
                    mContentType = GetValue(SqlReader, 12, typeOfColumn.GetInt32)
                    mType = GetValue(SqlReader, 13, typeOfColumn.GetInt32)
                    mShowDefault = GetValue(SqlReader, 14, typeOfColumn.GetInt32)
                    mUserCreate = GetValue(SqlReader, 15, typeOfColumn.GetString)
                    mLinkMenu = GetValue(SqlReader, 16, typeOfColumn.GetString)
                    mOrderPosition = GetValue(SqlReader, 17, typeOfColumn.GetInt32)
                    mSummary = GetValue(SqlReader, 18, typeOfColumn.GetString)
                    mImagesURL = GetValue(SqlReader, 19, typeOfColumn.GetString)
                    mHotContent = GetValue(SqlReader, 20, typeOfColumn.GetInt32)
                    mTimeCreate = GetValue(SqlReader, 21, typeOfColumn.GetString)
                    mMetaKeywords = GetValue(SqlReader, 22, typeOfColumn.GetString)
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
                StrSQL = " SELECT * FROM [Content] WHERE [Link Menu] = '" & sLink & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mCategoryNo_ = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mGroupNo_ = GetValue(SqlReader, 2, typeOfColumn.GetString)
                    mTitle = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mDateCreate = GetValue(SqlReader, 4, typeOfColumn.GetString)
                    mPublished = GetValue(SqlReader, 5, typeOfColumn.GetInt32)
                    mContent = GetValue(SqlReader, 6, typeOfColumn.GetString)
                    mClicked = GetValue(SqlReader, 7, typeOfColumn.GetInt32)
                    mMenuDivisionNo_ = GetValue(SqlReader, 8, typeOfColumn.GetString)
                    mMenuCategoryNo_ = GetValue(SqlReader, 9, typeOfColumn.GetString)
                    mMenuGroupNo_ = GetValue(SqlReader, 10, typeOfColumn.GetString)
                    mItemNo_ = GetValue(SqlReader, 11, typeOfColumn.GetString)
                    mContentType = GetValue(SqlReader, 12, typeOfColumn.GetInt32)
                    mType = GetValue(SqlReader, 13, typeOfColumn.GetInt32)
                    mShowDefault = GetValue(SqlReader, 14, typeOfColumn.GetInt32)
                    mUserCreate = GetValue(SqlReader, 15, typeOfColumn.GetString)
                    mLinkMenu = GetValue(SqlReader, 16, typeOfColumn.GetString)
                    mOrderPosition = GetValue(SqlReader, 17, typeOfColumn.GetInt32)
                    mSummary = GetValue(SqlReader, 18, typeOfColumn.GetString)
                    mImagesURL = GetValue(SqlReader, 19, typeOfColumn.GetString)
                    mHotContent = GetValue(SqlReader, 20, typeOfColumn.GetInt32)
                    mTimeCreate = GetValue(SqlReader, 21, typeOfColumn.GetString)
                    mMetaKeywords = GetValue(SqlReader, 22, typeOfColumn.GetString)
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


        Public Function LoadByItem(ByVal sItemNo As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                
                StrSQL = " SELECT * FROM [Content] WHERE [Item No_] = '" & sItemNo & "' AND [Content Type] = 1 AND [Type] = 1"
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mCategoryNo_ = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mGroupNo_ = GetValue(SqlReader, 2, typeOfColumn.GetString)
                    mTitle = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mDateCreate = GetValue(SqlReader, 4, typeOfColumn.GetString)
                    mPublished = GetValue(SqlReader, 5, typeOfColumn.GetInt32)
                    mContent = GetValue(SqlReader, 6, typeOfColumn.GetString)
                    mClicked = GetValue(SqlReader, 7, typeOfColumn.GetInt32)
                    mMenuDivisionNo_ = GetValue(SqlReader, 8, typeOfColumn.GetString)
                    mMenuCategoryNo_ = GetValue(SqlReader, 9, typeOfColumn.GetString)
                    mMenuGroupNo_ = GetValue(SqlReader, 10, typeOfColumn.GetString)
                    mItemNo_ = GetValue(SqlReader, 11, typeOfColumn.GetString)
                    mContentType = GetValue(SqlReader, 12, typeOfColumn.GetInt32)
                    mType = GetValue(SqlReader, 13, typeOfColumn.GetInt32)
                    mShowDefault = GetValue(SqlReader, 14, typeOfColumn.GetInt32)
                    mUserCreate = GetValue(SqlReader, 15, typeOfColumn.GetString)
                    mLinkMenu = GetValue(SqlReader, 16, typeOfColumn.GetString)
                    mOrderPosition = GetValue(SqlReader, 17, typeOfColumn.GetInt32)
                    mSummary = GetValue(SqlReader, 18, typeOfColumn.GetString)
                    mImagesURL = GetValue(SqlReader, 19, typeOfColumn.GetString)
                    mHotContent = GetValue(SqlReader, 20, typeOfColumn.GetInt32)
                    mTimeCreate = GetValue(SqlReader, 21, typeOfColumn.GetString)
                    mMetaKeywords = GetValue(SqlReader, 22, typeOfColumn.GetString)
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

        Public Function LoadConsultant(ByVal sCategoryNo As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Content] WHERE [Menu Group No_] = '" & sCategoryNo & "' AND [Group No_] = '03' AND Published = 1"
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mCategoryNo_ = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mGroupNo_ = GetValue(SqlReader, 2, typeOfColumn.GetString)
                    mTitle = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mDateCreate = GetValue(SqlReader, 4, typeOfColumn.GetString)
                    mPublished = GetValue(SqlReader, 5, typeOfColumn.GetInt32)
                    mContent = GetValue(SqlReader, 6, typeOfColumn.GetString)
                    mClicked = GetValue(SqlReader, 7, typeOfColumn.GetInt32)
                    mMenuDivisionNo_ = GetValue(SqlReader, 8, typeOfColumn.GetString)
                    mMenuCategoryNo_ = GetValue(SqlReader, 9, typeOfColumn.GetString)
                    mMenuGroupNo_ = GetValue(SqlReader, 10, typeOfColumn.GetString)
                    mItemNo_ = GetValue(SqlReader, 11, typeOfColumn.GetString)
                    mContentType = GetValue(SqlReader, 12, typeOfColumn.GetInt32)
                    mType = GetValue(SqlReader, 13, typeOfColumn.GetInt32)
                    mShowDefault = GetValue(SqlReader, 14, typeOfColumn.GetInt32)
                    mUserCreate = GetValue(SqlReader, 15, typeOfColumn.GetString)
                    mLinkMenu = GetValue(SqlReader, 16, typeOfColumn.GetString)
                    mOrderPosition = GetValue(SqlReader, 17, typeOfColumn.GetInt32)
                    mSummary = GetValue(SqlReader, 18, typeOfColumn.GetString)
                    mImagesURL = GetValue(SqlReader, 19, typeOfColumn.GetString)
                    mHotContent = GetValue(SqlReader, 20, typeOfColumn.GetInt32)
                    mTimeCreate = GetValue(SqlReader, 21, typeOfColumn.GetString)
                    mMetaKeywords = GetValue(SqlReader, 22, typeOfColumn.GetString)
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
            Dim arParams() As IDataParameter = New IDataParameter(21) {}
            Try
                arParams(0) = DBHelper.createParameter("@No_", SqlDbType.NVarChar, ParameterDirection.Input, mNo_)
                arParams(1) = DBHelper.createParameter("@CategoryNo_", SqlDbType.NVarChar, ParameterDirection.Input, mCategoryNo_)
                arParams(2) = DBHelper.createParameter("@GroupNo_", SqlDbType.NVarChar, ParameterDirection.Input, mGroupNo_)
                arParams(3) = DBHelper.createParameter("@Title", SqlDbType.NVarChar, ParameterDirection.Input, mTitle)
                arParams(4) = DBHelper.createParameter("@DateCreate", SqlDbType.NVarChar, ParameterDirection.Input, mDateCreate)
                arParams(5) = DBHelper.createParameter("@Published", SqlDbType.Int, ParameterDirection.Input, mPublished)
                arParams(6) = DBHelper.createParameter("@Content", SqlDbType.NVarChar, ParameterDirection.Input, mContent)
                arParams(7) = DBHelper.createParameter("@Clicked", SqlDbType.Int, ParameterDirection.Input, mClicked)
                arParams(8) = DBHelper.createParameter("@MenuDivisionNo_", SqlDbType.NVarChar, ParameterDirection.Input, mMenuDivisionNo_)
                arParams(9) = DBHelper.createParameter("@MenuCategoryNo_", SqlDbType.NVarChar, ParameterDirection.Input, mMenuCategoryNo_)
                arParams(10) = DBHelper.createParameter("@MenuGroupNo_", SqlDbType.NVarChar, ParameterDirection.Input, mMenuGroupNo_)
                arParams(11) = DBHelper.createParameter("@ItemNo_", SqlDbType.NVarChar, ParameterDirection.Input, mItemNo_)
                arParams(12) = DBHelper.createParameter("@ContentType", SqlDbType.Int, ParameterDirection.Input, mContentType)
                arParams(13) = DBHelper.createParameter("@Type", SqlDbType.Int, ParameterDirection.Input, mType)
                arParams(14) = DBHelper.createParameter("@ShowDefault", SqlDbType.Int, ParameterDirection.Input, mShowDefault)
                arParams(15) = DBHelper.createParameter("@UserCreate", SqlDbType.NVarChar, ParameterDirection.Input, mUserCreate)
                arParams(16) = DBHelper.createParameter("@LinkMenu", SqlDbType.NVarChar, ParameterDirection.Input, mLinkMenu)
                arParams(17) = DBHelper.createParameter("@OrderPosition", SqlDbType.Int, ParameterDirection.Input, mOrderPosition)
                arParams(18) = DBHelper.createParameter("@Summary", SqlDbType.NVarChar, ParameterDirection.Input, mSummary)
                arParams(19) = DBHelper.createParameter("@ImagesURL", SqlDbType.NVarChar, ParameterDirection.Input, mImagesURL)
                arParams(20) = DBHelper.createParameter("@HotContent", SqlDbType.Int, ParameterDirection.Input, mHotContent)
                arParams(21) = DBHelper.createParameter("@MetaKeywords", SqlDbType.Int, ParameterDirection.Input, mMetaKeywords)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_Content", arParams)
                Return True
            Catch ex As Exception

                Return False
            End Try
        End Function

        Public Sub SetBlank()
            mNo_ = ""
            mCategoryNo_ = ""
            mGroupNo_ = ""
            mTitle = ""
            mDateCreate = ""
            mPublished = 0
            mContent = ""
            mClicked = 0
            mMenuDivisionNo_ = ""
            mMenuCategoryNo_ = ""
            mMenuGroupNo_ = ""
            mItemNo_ = ""
            mContentType = 0
            mType = 0
            mShowDefault = 0
            mUserCreate = ""
            mLinkMenu = ""
            mOrderPosition = 0
            mMetaKeywords = ""
        End Sub

        Sub UpdateView(ByVal sNo As String)
            Dim SQL As String
            SQL = " UPDATE Content SET Clicked = ISNULL(Clicked,0) + 1 WHERE No_ = '" & sNo & "'"
            DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
        End Sub


#End Region

    End Class
End Namespace