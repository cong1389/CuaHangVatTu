Imports System.Data
Imports Microsoft.VisualBasic

Namespace adv.Business
    Public Class Modules

#Region "Khai báo"
        Private mNo_ As String = ""
        Private mName As String = ""
        Private mShowTitle As Integer = 0
        Private mPostionNo_ As String = ""
        Private mOrderGroup As Integer = 0
        Private mPublished As Integer = 0
        Private mNumOfColumn As Integer = 0
        Private mNumOfItem As Integer = 0
        Private mTitleImages As Integer = 0
        Private mAlign As Integer = 0
        Private mMaxWidth As Integer = 0
        Private mMaxHeight As Integer = 0
        Private mBorderImage As String = ""
        Private mBorderColor As String = ""
        Private mLinkStyle As Integer = 0
        Private mShowSlide As Integer = 0
        Private mSlideDelay As Integer = 0
        Private mSeparateBar As String = ""
        Private mModulesType As Integer = 0
        Private mCssClass As String = ""

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
        Property ShowTitle() As Integer
            Get
                Return mShowTitle
            End Get
            Set(ByVal value As Integer)
                mShowTitle = value
            End Set
        End Property
        Property PostionNo_() As String
            Get
                Return mPostionNo_
            End Get
            Set(ByVal value As String)
                mPostionNo_ = value
            End Set
        End Property
        Property OrderGroup() As Integer
            Get
                Return mOrderGroup
            End Get
            Set(ByVal value As Integer)
                mOrderGroup = value
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
        Property NumOfColumn() As Integer
            Get
                Return mNumOfColumn
            End Get
            Set(ByVal value As Integer)
                mNumOfColumn = value
            End Set
        End Property
        Property NumOfItem() As Integer
            Get
                Return mNumOfItem
            End Get
            Set(ByVal value As Integer)
                mNumOfItem = value
            End Set
        End Property
        Property TitleImages() As Integer
            Get
                Return mTitleImages
            End Get
            Set(ByVal value As Integer)
                mTitleImages = value
            End Set
        End Property
        Property Align() As Integer
            Get
                Return mAlign
            End Get
            Set(ByVal value As Integer)
                mAlign = value
            End Set
        End Property
        Property MaxWidth() As Integer
            Get
                Return mMaxWidth
            End Get
            Set(ByVal value As Integer)
                mMaxWidth = value
            End Set
        End Property
        Property MaxHeight() As Integer
            Get
                Return mMaxHeight
            End Get
            Set(ByVal value As Integer)
                mMaxHeight = value
            End Set
        End Property
        Property BorderImage() As String
            Get
                Return mBorderImage
            End Get
            Set(ByVal value As String)
                mBorderImage = value
            End Set
        End Property
        Property BorderColor() As String
            Get
                Return mBorderColor
            End Get
            Set(ByVal value As String)
                mBorderColor = value
            End Set
        End Property
        Property LinkStyle() As Integer
            Get
                Return mLinkStyle
            End Get
            Set(ByVal value As Integer)
                mLinkStyle = value
            End Set
        End Property
        Property ShowSlide() As Integer
            Get
                Return mShowSlide
            End Get
            Set(ByVal value As Integer)
                mShowSlide = value
            End Set
        End Property
        Property SlideDelay() As Integer
            Get
                Return mSlideDelay
            End Get
            Set(ByVal value As Integer)
                mSlideDelay = value
            End Set
        End Property
        Property SeparateBar() As String
            Get
                Return mSeparateBar
            End Get
            Set(ByVal value As String)
                mSeparateBar = value
            End Set
        End Property
        Property ModulesType() As Integer
            Get
                Return mModulesType
            End Get
            Set(ByVal value As Integer)
                mModulesType = value
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

#End Region
#Region "Function"
        Public Function Load(ByVal sNo As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Modules] WHERE No_ = '" & sNo & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mName = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mShowTitle = GetValue(SqlReader, 2, typeOfColumn.GetInt32)
                    mPostionNo_ = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mOrderGroup = GetValue(SqlReader, 4, typeOfColumn.GetInt32)
                    mPublished = GetValue(SqlReader, 5, typeOfColumn.GetInt32)
                    mNumOfColumn = GetValue(SqlReader, 6, typeOfColumn.GetInt32)
                    mNumOfItem = GetValue(SqlReader, 7, typeOfColumn.GetInt32)
                    mTitleImages = GetValue(SqlReader, 8, typeOfColumn.GetInt32)
                    mAlign = GetValue(SqlReader, 9, typeOfColumn.GetInt32)
                    mMaxWidth = GetValue(SqlReader, 10, typeOfColumn.GetInt32)
                    mMaxHeight = GetValue(SqlReader, 11, typeOfColumn.GetInt32)
                    mBorderImage = GetValue(SqlReader, 12, typeOfColumn.GetString)
                    mBorderColor = GetValue(SqlReader, 13, typeOfColumn.GetString)
                    mLinkStyle = GetValue(SqlReader, 14, typeOfColumn.GetInt32)
                    mShowSlide = GetValue(SqlReader, 15, typeOfColumn.GetInt32)
                    mSlideDelay = GetValue(SqlReader, 16, typeOfColumn.GetInt32)
                    mSeparateBar = GetValue(SqlReader, 17, typeOfColumn.GetString)
                    mModulesType = GetValue(SqlReader, 18, typeOfColumn.GetInt32)
                    mCssClass = GetValue(SqlReader, 19, typeOfColumn.GetString)
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

        Public Function LoadByPosition(ByVal sPosition As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Modules] WHERE [Postion No_] = '" & sPosition & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mName = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mShowTitle = GetValue(SqlReader, 2, typeOfColumn.GetInt32)
                    mPostionNo_ = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mOrderGroup = GetValue(SqlReader, 4, typeOfColumn.GetInt32)
                    mPublished = GetValue(SqlReader, 5, typeOfColumn.GetInt32)
                    mNumOfColumn = GetValue(SqlReader, 6, typeOfColumn.GetInt32)
                    mNumOfItem = GetValue(SqlReader, 7, typeOfColumn.GetInt32)
                    mTitleImages = GetValue(SqlReader, 8, typeOfColumn.GetInt32)
                    mAlign = GetValue(SqlReader, 9, typeOfColumn.GetInt32)
                    mMaxWidth = GetValue(SqlReader, 10, typeOfColumn.GetInt32)
                    mMaxHeight = GetValue(SqlReader, 11, typeOfColumn.GetInt32)
                    mBorderImage = GetValue(SqlReader, 12, typeOfColumn.GetString)
                    mBorderColor = GetValue(SqlReader, 13, typeOfColumn.GetString)
                    mLinkStyle = GetValue(SqlReader, 14, typeOfColumn.GetInt32)
                    mShowSlide = GetValue(SqlReader, 15, typeOfColumn.GetInt32)
                    mSlideDelay = GetValue(SqlReader, 16, typeOfColumn.GetInt32)
                    mSeparateBar = GetValue(SqlReader, 17, typeOfColumn.GetString)
                    mModulesType = GetValue(SqlReader, 18, typeOfColumn.GetInt32)
                    mCssClass = GetValue(SqlReader, 19, typeOfColumn.GetString)
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
                arParams(2) = DBHelper.createParameter("@ShowTitle", SqlDbType.Int, ParameterDirection.Input, mShowTitle)
                arParams(3) = DBHelper.createParameter("@PostionNo_", SqlDbType.NVarChar, ParameterDirection.Input, mPostionNo_)
                arParams(4) = DBHelper.createParameter("@OrderGroup", SqlDbType.Int, ParameterDirection.Input, mOrderGroup)
                arParams(5) = DBHelper.createParameter("@Published", SqlDbType.Int, ParameterDirection.Input, mPublished)
                arParams(6) = DBHelper.createParameter("@NumOfColumn", SqlDbType.Int, ParameterDirection.Input, mNumOfColumn)
                arParams(7) = DBHelper.createParameter("@NumOfItem", SqlDbType.Int, ParameterDirection.Input, mNumOfItem)
                arParams(8) = DBHelper.createParameter("@TitleImages", SqlDbType.Int, ParameterDirection.Input, mTitleImages)
                arParams(9) = DBHelper.createParameter("@Align", SqlDbType.Int, ParameterDirection.Input, mAlign)
                arParams(10) = DBHelper.createParameter("@MaxWidth", SqlDbType.Int, ParameterDirection.Input, mMaxWidth)
                arParams(11) = DBHelper.createParameter("@MaxHeight", SqlDbType.Int, ParameterDirection.Input, mMaxHeight)
                arParams(12) = DBHelper.createParameter("@BorderImage", SqlDbType.NVarChar, ParameterDirection.Input, mBorderImage)
                arParams(13) = DBHelper.createParameter("@BorderColor", SqlDbType.NVarChar, ParameterDirection.Input, mBorderColor)
                arParams(14) = DBHelper.createParameter("@LinkStyle", SqlDbType.Int, ParameterDirection.Input, mLinkStyle)
                arParams(15) = DBHelper.createParameter("@ShowSlide", SqlDbType.Int, ParameterDirection.Input, mShowSlide)
                arParams(16) = DBHelper.createParameter("@SlideDelay", SqlDbType.Int, ParameterDirection.Input, mSlideDelay)
                arParams(17) = DBHelper.createParameter("@SeparateBar", SqlDbType.NVarChar, ParameterDirection.Input, mSeparateBar)
                arParams(18) = DBHelper.createParameter("@ModulesType", SqlDbType.Int, ParameterDirection.Input, mModulesType)
                arParams(19) = DBHelper.createParameter("@CssClass", SqlDbType.NVarChar, ParameterDirection.Input, mCssClass)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_Modules", arParams)
                Return True
            Catch ex As Exception

                Return False
            End Try
        End Function
#End Region

    End Class
End Namespace
