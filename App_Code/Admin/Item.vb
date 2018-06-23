Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OleDb

Namespace adv.Business
    Public Class Item
#Region "Khai báo"
        Private mNo_ As String = ""
        Private mName As String = ""
        Private mModel As String = ""
        Private mBrandNo_ As String = ""
        Private mDivisionNo_ As String = ""
        Private mCategoryNo_ As String = ""
        Private mProducGroupNo_ As String = ""
        Private mMenuDivisionNo_ As String = ""
        Private mMenuCategoryNo_ As String = ""
        Private mMenuGroupNo_ As String = ""
        Private mPromotionsProduct As Integer = 0
        Private mBestSelling As Integer = 0
        Private mNewProduct As Integer = 0
        Private mHideFeature As Integer = 0
        Private mNotInStock As Integer = 0
        Private mVATPercent As Integer = 0
        Private mDescriptions As String = ""
        Private mPublished As Integer = 0
        Private mSalesPrice As Double = 0
        Private mPriceIclVAT As Integer = 0
        Private mImagesURL As String = ""
        Private mImagesThumURL As String = ""
        Private mPageTitle As String = ""
        Private mMetaKeywords As String = ""
        Private mLastDateModify As String = ""
        Private mUserID As String = ""
        Private mStock As Integer = 0
        Private mOriginCountry As String = ""
        Private mOrderPosition As Integer = 0
        Private mHotProduct As Integer = 0
        Private mVATGroup As String = ""
        Private mFromDate As String = ""
        Private mItemSameModel As String = ""
        Private mUnitOfMeasure As String = ""
        Private mGoodGoingOn As Integer = 0
        Private mNumClick As Integer = 0
        Private mFlash360Url As String = ""
        Private mLinkUrl As String = ""
        Private mPage As String = ""
        Private mPricebyUom As Integer = 0
        Private mSalebyVariant As Integer = 0
        Private mVolume As Double = 0
        Private mIsRun As Integer = 0

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
        Property Model() As String
            Get
                Return mModel
            End Get
            Set(ByVal value As String)
                mModel = value
            End Set
        End Property
        Property BrandNo_() As String
            Get
                Return mBrandNo_
            End Get
            Set(ByVal value As String)
                mBrandNo_ = value
            End Set
        End Property
        Property DivisionNo_() As String
            Get
                Return mDivisionNo_
            End Get
            Set(ByVal value As String)
                mDivisionNo_ = value
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
        Property ProducGroupNo_() As String
            Get
                Return mProducGroupNo_
            End Get
            Set(ByVal value As String)
                mProducGroupNo_ = value
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
        Property PromotionsProduct() As Integer
            Get
                Return mPromotionsProduct
            End Get
            Set(ByVal value As Integer)
                mPromotionsProduct = value
            End Set
        End Property
        Property BestSelling() As Integer
            Get
                Return mBestSelling
            End Get
            Set(ByVal value As Integer)
                mBestSelling = value
            End Set
        End Property
        Property NewProduct() As Integer
            Get
                Return mNewProduct
            End Get
            Set(ByVal value As Integer)
                mNewProduct = value
            End Set
        End Property
        Property HideFeature() As Integer
            Get
                Return mHideFeature
            End Get
            Set(ByVal value As Integer)
                mHideFeature = value
            End Set
        End Property
        Property NotInStock() As Integer
            Get
                Return mNotInStock
            End Get
            Set(ByVal value As Integer)
                mNotInStock = value
            End Set
        End Property
        Property VATPercent() As Integer
            Get
                Return mVATPercent
            End Get
            Set(ByVal value As Integer)
                mVATPercent = value
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
        Property Published() As Integer
            Get
                Return mPublished
            End Get
            Set(ByVal value As Integer)
                mPublished = value
            End Set
        End Property
        Property SalesPrice() As Double
            Get
                Return mSalesPrice
            End Get
            Set(ByVal value As Double)
                mSalesPrice = value
            End Set
        End Property
        Property PriceIclVAT() As Integer
            Get
                Return mPriceIclVAT
            End Get
            Set(ByVal value As Integer)
                mPriceIclVAT = value
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
        Property PageTitle() As String
            Get
                Return mPageTitle
            End Get
            Set(ByVal value As String)
                mPageTitle = value
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
        Property LastDateModify() As String
            Get
                Return mLastDateModify
            End Get
            Set(ByVal value As String)
                mLastDateModify = value
            End Set
        End Property
        Property UserID() As String
            Get
                Return mUserID
            End Get
            Set(ByVal value As String)
                mUserID = value
            End Set
        End Property
        Property Stock() As Double
            Get
                Return mStock
            End Get
            Set(ByVal value As Double)
                mStock = value
            End Set
        End Property
        Property OriginCountry() As String
            Get
                Return mOriginCountry
            End Get
            Set(ByVal value As String)
                mOriginCountry = value
            End Set
        End Property
        Property OrderPosition() As Integer
            Get
                Return mOrderPosition
            End Get
            Set(ByVal value As Integer)
                mOrderPosition = value
            End Set
        End Property
        Property HotProduct() As Integer
            Get
                Return mHotProduct
            End Get
            Set(ByVal value As Integer)
                mHotProduct = value
            End Set
        End Property
        Property VATGroup() As String
            Get
                Return mVATGroup
            End Get
            Set(ByVal value As String)
                mVATGroup = value
            End Set
        End Property
        Property FromDate() As String
            Get
                Return mFromDate
            End Get
            Set(ByVal value As String)
                mFromDate = value
            End Set
        End Property
        Property ItemSameModel As String
            Get
                Return mItemSameModel
            End Get
            Set(ByVal value As String)
                mItemSameModel = value
            End Set
        End Property
        Property UnitOfMeasure As String
            Get
                Return mUnitOfMeasure
            End Get
            Set(ByVal value As String)
                mUnitOfMeasure = value
            End Set
        End Property
        Property GoodGoingOn As Integer
            Get
                Return mGoodGoingOn
            End Get
            Set(ByVal value As Integer)
                mGoodGoingOn = value
            End Set
        End Property
        Property NumClick As Integer
            Get
                Return mNumClick
            End Get
            Set(ByVal value As Integer)
                mNumClick = value
            End Set
        End Property
        Property Flash360Url As String
            Get
                Return mFlash360Url
            End Get
            Set(ByVal value As String)
                mFlash360Url = value
            End Set
        End Property
        Property LinkUrl As String
            Get
                Return mLinkUrl
            End Get
            Set(ByVal value As String)
                mLinkUrl = value
            End Set
        End Property
        Property PageNo As String
            Get
                Return mPage
            End Get
            Set(ByVal value As String)
                mPage = value
            End Set
        End Property
        Property PricebyUom As Integer
            Get
                Return mPricebyUom
            End Get
            Set(ByVal value As Integer)
                mPricebyUom = value
            End Set
        End Property
        Property SalebyVariant As Integer
            Get
                Return mSalebyVariant
            End Get
            Set(ByVal value As Integer)
                mSalebyVariant = value
            End Set
        End Property
        Property Volume As Double
            Get
                Return mVolume
            End Get
            Set(ByVal value As Double)
                mVolume = value
            End Set
        End Property
        Property IsRun As Integer
            Get
                Return mIsRun
            End Get
            Set(ByVal value As Integer)
                mIsRun = value
            End Set
        End Property
#End Region
#Region "Function"
        Public Function Load(ByVal sNo_ As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Item] WHERE No_ = '" & sNo_ & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = adv.Business.GetValue(SqlReader, 0, adv.Business.typeOfColumn.GetString)
                    mName = adv.Business.GetValue(SqlReader, 1, adv.Business.typeOfColumn.GetString)
                    mModel = adv.Business.GetValue(SqlReader, 2, adv.Business.typeOfColumn.GetString)
                    mBrandNo_ = adv.Business.GetValue(SqlReader, 3, adv.Business.typeOfColumn.GetString)
                    mDivisionNo_ = adv.Business.GetValue(SqlReader, 4, adv.Business.typeOfColumn.GetString)
                    mCategoryNo_ = adv.Business.GetValue(SqlReader, 5, adv.Business.typeOfColumn.GetString)
                    mProducGroupNo_ = adv.Business.GetValue(SqlReader, 6, adv.Business.typeOfColumn.GetString)
                    mMenuDivisionNo_ = adv.Business.GetValue(SqlReader, 7, adv.Business.typeOfColumn.GetString)
                    mMenuCategoryNo_ = adv.Business.GetValue(SqlReader, 8, adv.Business.typeOfColumn.GetString)
                    mMenuGroupNo_ = adv.Business.GetValue(SqlReader, 9, adv.Business.typeOfColumn.GetString)
                    mPromotionsProduct = adv.Business.GetValue(SqlReader, 10, adv.Business.typeOfColumn.GetInt32)
                    mBestSelling = adv.Business.GetValue(SqlReader, 11, adv.Business.typeOfColumn.GetInt32)
                    mNewProduct = adv.Business.GetValue(SqlReader, 12, adv.Business.typeOfColumn.GetInt32)
                    mHideFeature = adv.Business.GetValue(SqlReader, 13, adv.Business.typeOfColumn.GetInt32)
                    mNotInStock = adv.Business.GetValue(SqlReader, 14, adv.Business.typeOfColumn.GetInt32)
                    mVATPercent = adv.Business.GetValue(SqlReader, 15, adv.Business.typeOfColumn.GetInt32)
                    mDescriptions = adv.Business.GetValue(SqlReader, 16, adv.Business.typeOfColumn.GetString)
                    mPublished = adv.Business.GetValue(SqlReader, 17, adv.Business.typeOfColumn.GetInt32)
                    mSalesPrice = adv.Business.GetValue(SqlReader, 18, adv.Business.typeOfColumn.GetDecimal)
                    mPriceIclVAT = adv.Business.GetValue(SqlReader, 19, adv.Business.typeOfColumn.GetInt32)
                    mImagesURL = adv.Business.GetValue(SqlReader, 20, adv.Business.typeOfColumn.GetString)
                    mImagesThumURL = adv.Business.GetValue(SqlReader, 21, adv.Business.typeOfColumn.GetString)
                    mPageTitle = adv.Business.GetValue(SqlReader, 22, adv.Business.typeOfColumn.GetString)
                    mMetaKeywords = adv.Business.GetValue(SqlReader, 23, adv.Business.typeOfColumn.GetString)
                    mLastDateModify = adv.Business.GetValue(SqlReader, 24, adv.Business.typeOfColumn.GetString)
                    mUserID = adv.Business.GetValue(SqlReader, 25, adv.Business.typeOfColumn.GetString)
                    mStock = adv.Business.GetValue(SqlReader, 26, adv.Business.typeOfColumn.GetDecimal)
                    mOriginCountry = adv.Business.GetValue(SqlReader, 27, adv.Business.typeOfColumn.GetString)
                    mOrderPosition = adv.Business.GetValue(SqlReader, 28, adv.Business.typeOfColumn.GetInt32)
                    mHotProduct = adv.Business.GetValue(SqlReader, 29, adv.Business.typeOfColumn.GetInt32)
                    mVATGroup = adv.Business.GetValue(SqlReader, 30, adv.Business.typeOfColumn.GetString)
                    mFromDate = adv.Business.GetValue(SqlReader, 31, adv.Business.typeOfColumn.GetString)
                    mItemSameModel = adv.Business.GetValue(SqlReader, 32, adv.Business.typeOfColumn.GetString)
                    mUnitOfMeasure = adv.Business.GetValue(SqlReader, 33, adv.Business.typeOfColumn.GetString)
                    mGoodGoingOn = adv.Business.GetValue(SqlReader, 34, adv.Business.typeOfColumn.GetInt32)
                    mNumClick = adv.Business.GetValue(SqlReader, 35, adv.Business.typeOfColumn.GetInt32)
                    mLinkUrl = adv.Business.GetValue(SqlReader, 36, adv.Business.typeOfColumn.GetString)
                    mPage = adv.Business.GetValue(SqlReader, 37, adv.Business.typeOfColumn.GetString)
                    mPricebyUom = adv.Business.GetValue(SqlReader, 38, adv.Business.typeOfColumn.GetInt32)
                    mSalebyVariant = adv.Business.GetValue(SqlReader, 39, adv.Business.typeOfColumn.GetInt32)
                    mVolume = adv.Business.GetValue(SqlReader, 40, adv.Business.typeOfColumn.GetDecimal)
                    mIsRun = adv.Business.GetValue(SqlReader, 41, adv.Business.typeOfColumn.GetInt32)
                    SqlReader.Close()
                    LoadFlashURL(sNo_)
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
                StrSQL = " SELECT * FROM [Item] WHERE [Link URL] = '" & sLink & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = adv.Business.GetValue(SqlReader, 0, adv.Business.typeOfColumn.GetString)
                    mName = adv.Business.GetValue(SqlReader, 1, adv.Business.typeOfColumn.GetString)
                    mModel = adv.Business.GetValue(SqlReader, 2, adv.Business.typeOfColumn.GetString)
                    mBrandNo_ = adv.Business.GetValue(SqlReader, 3, adv.Business.typeOfColumn.GetString)
                    mDivisionNo_ = adv.Business.GetValue(SqlReader, 4, adv.Business.typeOfColumn.GetString)
                    mCategoryNo_ = adv.Business.GetValue(SqlReader, 5, adv.Business.typeOfColumn.GetString)
                    mProducGroupNo_ = adv.Business.GetValue(SqlReader, 6, adv.Business.typeOfColumn.GetString)
                    mMenuDivisionNo_ = adv.Business.GetValue(SqlReader, 7, adv.Business.typeOfColumn.GetString)
                    mMenuCategoryNo_ = adv.Business.GetValue(SqlReader, 8, adv.Business.typeOfColumn.GetString)
                    mMenuGroupNo_ = adv.Business.GetValue(SqlReader, 9, adv.Business.typeOfColumn.GetString)
                    mPromotionsProduct = adv.Business.GetValue(SqlReader, 10, adv.Business.typeOfColumn.GetInt32)
                    mBestSelling = adv.Business.GetValue(SqlReader, 11, adv.Business.typeOfColumn.GetInt32)
                    mNewProduct = adv.Business.GetValue(SqlReader, 12, adv.Business.typeOfColumn.GetInt32)
                    mHideFeature = adv.Business.GetValue(SqlReader, 13, adv.Business.typeOfColumn.GetInt32)
                    mNotInStock = adv.Business.GetValue(SqlReader, 14, adv.Business.typeOfColumn.GetInt32)
                    mVATPercent = adv.Business.GetValue(SqlReader, 15, adv.Business.typeOfColumn.GetInt32)
                    mDescriptions = adv.Business.GetValue(SqlReader, 16, adv.Business.typeOfColumn.GetString)
                    mPublished = adv.Business.GetValue(SqlReader, 17, adv.Business.typeOfColumn.GetInt32)
                    mSalesPrice = adv.Business.GetValue(SqlReader, 18, adv.Business.typeOfColumn.GetDecimal)
                    mPriceIclVAT = adv.Business.GetValue(SqlReader, 19, adv.Business.typeOfColumn.GetInt32)
                    mImagesURL = adv.Business.GetValue(SqlReader, 20, adv.Business.typeOfColumn.GetString)
                    mImagesThumURL = adv.Business.GetValue(SqlReader, 21, adv.Business.typeOfColumn.GetString)
                    mPageTitle = adv.Business.GetValue(SqlReader, 22, adv.Business.typeOfColumn.GetString)
                    mMetaKeywords = adv.Business.GetValue(SqlReader, 23, adv.Business.typeOfColumn.GetString)
                    mLastDateModify = adv.Business.GetValue(SqlReader, 24, adv.Business.typeOfColumn.GetString)
                    mUserID = adv.Business.GetValue(SqlReader, 25, adv.Business.typeOfColumn.GetString)
                    mStock = adv.Business.GetValue(SqlReader, 26, adv.Business.typeOfColumn.GetDecimal)
                    mOriginCountry = adv.Business.GetValue(SqlReader, 27, adv.Business.typeOfColumn.GetString)
                    mOrderPosition = adv.Business.GetValue(SqlReader, 28, adv.Business.typeOfColumn.GetInt32)
                    mHotProduct = adv.Business.GetValue(SqlReader, 29, adv.Business.typeOfColumn.GetInt32)
                    mVATGroup = adv.Business.GetValue(SqlReader, 30, adv.Business.typeOfColumn.GetString)
                    mFromDate = adv.Business.GetValue(SqlReader, 31, adv.Business.typeOfColumn.GetString)
                    mItemSameModel = adv.Business.GetValue(SqlReader, 32, adv.Business.typeOfColumn.GetString)
                    mUnitOfMeasure = adv.Business.GetValue(SqlReader, 33, adv.Business.typeOfColumn.GetString)
                    mGoodGoingOn = adv.Business.GetValue(SqlReader, 34, adv.Business.typeOfColumn.GetInt32)
                    mNumClick = adv.Business.GetValue(SqlReader, 35, adv.Business.typeOfColumn.GetInt32)
                    mLinkUrl = adv.Business.GetValue(SqlReader, 36, adv.Business.typeOfColumn.GetString)
                    mPage = adv.Business.GetValue(SqlReader, 37, adv.Business.typeOfColumn.GetString)
                    mPricebyUom = adv.Business.GetValue(SqlReader, 38, adv.Business.typeOfColumn.GetInt32)
                    mSalebyVariant = adv.Business.GetValue(SqlReader, 39, adv.Business.typeOfColumn.GetInt32)
                    mVolume = adv.Business.GetValue(SqlReader, 40, adv.Business.typeOfColumn.GetDecimal)
                    mIsRun = adv.Business.GetValue(SqlReader, 41, adv.Business.typeOfColumn.GetInt32)
                    SqlReader.Close()
                    LoadFlashURL(mNo_)
                    Return True
                Else
                    SqlReader.Close()
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Sub LoadFlashURL(ByVal sItemNo As String)
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT IsNULL([Flash URL],'') [Flash URL] FROM [Item Flash 360] WHERE [Item No_] = '" & sItemNo & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                mFlash360Url = ""
                If SqlReader.Read() Then
                    mFlash360Url = adv.Business.GetValue(SqlReader, 0, adv.Business.typeOfColumn.GetString)
                End If
                SqlReader.Close()
            Catch ex As Exception

            End Try
        End Sub

        Public Function Save() As Boolean
            Dim arParams() As IDataParameter = New IDataParameter(40) {}
            Try
                arParams(0) = DBHelper.createParameter("@No_", SqlDbType.NVarChar, ParameterDirection.Input, mNo_)
                arParams(1) = DBHelper.createParameter("@Name", SqlDbType.NVarChar, ParameterDirection.Input, mName)
                arParams(2) = DBHelper.createParameter("@Model", SqlDbType.NVarChar, ParameterDirection.Input, mModel)
                arParams(3) = DBHelper.createParameter("@BrandNo_", SqlDbType.NVarChar, ParameterDirection.Input, mBrandNo_)
                arParams(4) = DBHelper.createParameter("@DivisionNo_", SqlDbType.NVarChar, ParameterDirection.Input, mDivisionNo_)
                arParams(5) = DBHelper.createParameter("@CategoryNo_", SqlDbType.NVarChar, ParameterDirection.Input, mCategoryNo_)
                arParams(6) = DBHelper.createParameter("@ProducGroupNo_", SqlDbType.NVarChar, ParameterDirection.Input, mProducGroupNo_)
                arParams(7) = DBHelper.createParameter("@MenuDivisionNo_", SqlDbType.NVarChar, ParameterDirection.Input, mMenuDivisionNo_)
                arParams(8) = DBHelper.createParameter("@MenuCategoryNo_", SqlDbType.NVarChar, ParameterDirection.Input, mMenuCategoryNo_)
                arParams(9) = DBHelper.createParameter("@MenuGroupNo_", SqlDbType.NVarChar, ParameterDirection.Input, mMenuGroupNo_)
                arParams(10) = DBHelper.createParameter("@PromotionsProduct", SqlDbType.Int, ParameterDirection.Input, mPromotionsProduct)
                arParams(11) = DBHelper.createParameter("@BestSelling", SqlDbType.Int, ParameterDirection.Input, mBestSelling)
                arParams(12) = DBHelper.createParameter("@NewProduct", SqlDbType.Int, ParameterDirection.Input, mNewProduct)
                arParams(13) = DBHelper.createParameter("@HideFeature", SqlDbType.Int, ParameterDirection.Input, mHideFeature)
                arParams(14) = DBHelper.createParameter("@NotInStock", SqlDbType.Int, ParameterDirection.Input, mNotInStock)
                arParams(15) = DBHelper.createParameter("@VATPercent", SqlDbType.Int, ParameterDirection.Input, mVATPercent)
                arParams(16) = DBHelper.createParameter("@Descriptions", SqlDbType.NVarChar, ParameterDirection.Input, mDescriptions)
                arParams(17) = DBHelper.createParameter("@Published", SqlDbType.Int, ParameterDirection.Input, mPublished)
                arParams(18) = DBHelper.createParameter("@SalesPrice", SqlDbType.Decimal, ParameterDirection.Input, mSalesPrice)
                arParams(19) = DBHelper.createParameter("@PriceIclVAT", SqlDbType.Int, ParameterDirection.Input, mPriceIclVAT)
                arParams(20) = DBHelper.createParameter("@ImagesURL", SqlDbType.NVarChar, ParameterDirection.Input, mImagesURL)
                arParams(21) = DBHelper.createParameter("@ImagesThumURL", SqlDbType.NVarChar, ParameterDirection.Input, mImagesThumURL)
                arParams(22) = DBHelper.createParameter("@PageTitle", SqlDbType.NVarChar, ParameterDirection.Input, mPageTitle)
                arParams(23) = DBHelper.createParameter("@MetaKeywords", SqlDbType.NVarChar, ParameterDirection.Input, mMetaKeywords)
                arParams(24) = DBHelper.createParameter("@LastDateModify", SqlDbType.NVarChar, ParameterDirection.Input, mLastDateModify)
                arParams(25) = DBHelper.createParameter("@UserID", SqlDbType.NVarChar, ParameterDirection.Input, mUserID)
                arParams(26) = DBHelper.createParameter("@Stock", SqlDbType.Decimal, ParameterDirection.Input, mStock)
                arParams(27) = DBHelper.createParameter("@OriginCountry", SqlDbType.NVarChar, ParameterDirection.Input, mOriginCountry)
                arParams(28) = DBHelper.createParameter("@OrderPosition", SqlDbType.Int, ParameterDirection.Input, mOrderPosition)
                arParams(29) = DBHelper.createParameter("@HotProduct", SqlDbType.Int, ParameterDirection.Input, mHotProduct)
                arParams(30) = DBHelper.createParameter("@VATGroup", SqlDbType.NVarChar, ParameterDirection.Input, mVATGroup)
                arParams(31) = DBHelper.createParameter("@FromDate", SqlDbType.NVarChar, ParameterDirection.Input, mFromDate)
                arParams(32) = DBHelper.createParameter("@ItemSameModel", SqlDbType.NVarChar, ParameterDirection.Input, mItemSameModel)
                arParams(33) = DBHelper.createParameter("@UnitOfMeasure", SqlDbType.NVarChar, ParameterDirection.Input, mUnitOfMeasure)
                arParams(34) = DBHelper.createParameter("@GoodGoingOn", SqlDbType.Int, ParameterDirection.Input, mGoodGoingOn)
                arParams(35) = DBHelper.createParameter("@mNumClick", SqlDbType.Int, ParameterDirection.Input, mNumClick)
                arParams(36) = DBHelper.createParameter("@LinkURL", SqlDbType.Int, ParameterDirection.Input, mLinkUrl)
                arParams(37) = DBHelper.createParameter("@PricebyUom", SqlDbType.Int, ParameterDirection.Input, mPricebyUom)
                arParams(38) = DBHelper.createParameter("@SalebyVariant", SqlDbType.Int, ParameterDirection.Input, mSalebyVariant)
                arParams(39) = DBHelper.createParameter("@Volume", SqlDbType.Decimal, ParameterDirection.Input, mVolume)
                arParams(40) = DBHelper.createParameter("@IsRun", SqlDbType.Int, ParameterDirection.Input, mIsRun)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_Item", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Function CopyNewItem(ByVal sItemFrom As String, ByVal sItemNo As String) As Boolean
            Dim arParams() As IDataParameter = New IDataParameter(1) {}
            Try
                arParams(0) = DBHelper.createParameter("@ItemNoFrom", SqlDbType.NVarChar, ParameterDirection.Input, sItemFrom)
                arParams(1) = DBHelper.createParameter("@ItemNoTo", SqlDbType.NVarChar, ParameterDirection.Input, sItemNo)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_CopyItem", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Function GetItemSamePrice(ByVal sItemNo As String) As DataTable
            Dim SQL As String
            SQL = " SELECT TOP 10 No_ FROM Item WHERE [Produc Group No_] IN (SELECT [Produc Group No_] FROM Item WHERE No_ = '" & sItemNo & "') AND Published = 1 "
            Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        End Function

        Public Sub SetBlank()
            mNo_ = ""
            mName = ""
            mModel = ""
            mBrandNo_ = ""
            mDivisionNo_ = ""
            mCategoryNo_ = ""
            mProducGroupNo_ = ""
            mMenuDivisionNo_ = ""
            mMenuCategoryNo_ = ""
            mMenuGroupNo_ = ""
            mPromotionsProduct = 0
            mBestSelling = 0
            mNewProduct = 0
            mHideFeature = 0
            mNotInStock = 0
            mVATPercent = 0
            mDescriptions = ""
            mPublished = 0
            mSalesPrice = 0
            mPriceIclVAT = 0
            mImagesURL = ""
            mImagesThumURL = ""
            mPageTitle = ""
            mMetaKeywords = ""
            mLastDateModify = ""
            mUserID = ""
            mStock = 0
            mOriginCountry = ""
            mOrderPosition = 0
            mHotProduct = 0
            mVATGroup = ""
            mFromDate = ""
            mItemSameModel = ""
            mUnitOfMeasure = ""
            mGoodGoingOn = 0
            mNumClick = 0
            mLinkUrl = ""
            mIsRun = 0
        End Sub

        Sub GetSalesPrice(ByVal sItemNo As String, ByRef nOriginPrice As Double, ByRef nDealPrice As Double, ByRef sGiftDecriptions As String, ByRef sPromotionNo As String, Optional ByVal UnitOfMeasure As String = "")

            Dim sToday As String = Date2Char(getToday())
            Dim SQL As String = ""
            Dim SqlReader As IDataReader
            nOriginPrice = 0
            nDealPrice = 0
            sGiftDecriptions = ""
            Dim sDiscountNo As String = ""
            Load(sItemNo)
            If UnitOfMeasure.Trim = "" Then
                UnitOfMeasure = mUnitOfMeasure
            End If
            SQL = "SELECT TOP 1 [Origin Price], [Deal Price] FROM [Sales Price] " & _
                    " WHERE [Item No_] = '" & sItemNo & "' AND [Starting Date] <= '" & sToday & "' AND ([Ending Date] >= '" & sToday & "' OR [Ending Date] = '') ORDER BY [Deal Price] DESC"

            SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, SQL)
            If SqlReader.Read() Then
                nOriginPrice = SqlReader.GetDecimal(0)
                nDealPrice = SqlReader.GetDecimal(1)
                SqlReader.Close()
            Else
                SqlReader.Close()
                nOriginPrice = mSalesPrice
            End If

        End Sub

        Function GetGift(ByVal sPromotionNo As String) As DataTable
            Dim SQL As String
            SQL = " SELECT [Item No_], Quantity, [Origin Price], [Deal Price], [Unit Of Measure], [Disc_ Type] FROM [Discount Line] WHERE [Discount No_] = '" & sPromotionNo & "' AND [Trigger Discount] = 0 "
            Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        End Function

        Function GetFirstDiscountNo(ByVal sItemNo As String, ByVal sDate As String) As String
            Dim SQL As String
            SQL = " SELECT TOP 1 D.[Discount No_] " & _
                    " FROM [Discount Line] D  " & _
                    " WHERE D.[Item No_] = '" & sItemNo & "' AND D.[Starting Date] <= '" & sDate & "' AND (D.[Ending Date] >= '" & sDate & "' OR D.[Ending Date] = '' ) AND D.[Trigger Discount] = 1 " & _
                    " ORDER BY (D.[Origin Price] - D.[Deal Price]) DESC "
            Return ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), "").ToString.Trim
        End Function

        Sub Click(ByVal sItemNo As String)
            Dim SQL As String
            SQL = "UPDATE Item SET [Num Click] = ISNULL([Num Click],0) + 1  WHERE No_ = '" & sItemNo & "'"
            DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
        End Sub

        Sub InsertFlash360Url(ByVal sItemNo As String, ByVal sFlash360Url As String)
            Dim SQL As String
            SQL = " DELETE FROM [Item Flash 360] WHERE [Item No_] = '" & sItemNo & "'"
            DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
            SQL = " INSERT INTO [Item Flash 360] ([Item No_], [Flash URL]) VALUES ('" & sItemNo & "','" & sFlash360Url & "')"
            DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
        End Sub

        Sub DeleteFlash360Url(ByVal sItemNo As String)
            Dim SQL As String
            SQL = " DELETE FROM [Item Flash 360] WHERE [Item No_] = '" & sItemNo & "'"
            DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
        End Sub

        Function GetAllPromotions(ByVal sItemNo As String) As DataTable
            Dim SQL As String = "", sToday As String
            sToday = Date2Char(getToday())
            Load(sItemNo)
            SQL = " SELECT [Promotion Type], Name, [Description] FROM Promotions " & _
                    " WHERE [Starting Date] <= '" & sToday & "' AND ([Ending Date] >= '" & sToday & "' OR [Ending Date] = '') AND [Menu No_] = '" & mMenuDivisionNo_ & "' " & _
                    " AND RTRIM([Brand No_]) = '' AND RTRIM([Item No_]) = '' " & _
                    " UNION ALL " & _
                    " SELECT [Promotion Type], Name, [Description] FROM Promotions " & _
                    " WHERE [Starting Date] <= '" & sToday & "' AND ([Ending Date] >= '" & sToday & "' OR [Ending Date] = '') AND [Menu No_] = '" & mMenuCategoryNo_ & "' " & _
                    " AND RTRIM([Brand No_]) = '' AND RTRIM([Item No_]) = '' " & _
                    " UNION ALL " & _
                    " SELECT [Promotion Type], Name, [Description] FROM Promotions " & _
                    " WHERE [Starting Date] <= '" & sToday & "' AND ([Ending Date] >= '" & sToday & "' OR [Ending Date] = '') AND [Menu No_] = '" & mMenuGroupNo_ & "' " & _
                    " AND RTRIM([Brand No_]) = '' AND RTRIM([Item No_]) = '' " & _
                    " UNION ALL " & _
                    " SELECT [Promotion Type], Name, [Description] FROM Promotions " & _
                    " WHERE [Starting Date] <= '" & sToday & "' AND ([Ending Date] >= '" & sToday & "' OR [Ending Date] = '') AND [Item No_] = '" & sItemNo & "'" & _
                    " AND RTRIM([Menu No_]) = '' AND RTRIM([Brand No_]) = '' " & _
                    " UNION ALL " & _
                    " SELECT [Promotion Type], Name, [Description] FROM Promotions " & _
                    " WHERE [Starting Date] <= '" & sToday & "' AND ([Ending Date] >= '" & sToday & "' OR [Ending Date] = '') AND [Brand No_] = '" & mBrandNo_ & "' " & _
                    " AND RTRIM([Menu No_]) = '' AND RTRIM([Item No_]) = '' " & _
                    " UNION ALL " & _
                    " SELECT [Promotion Type], Name, [Description] FROM Promotions " & _
                    " WHERE [Starting Date] <= '" & sToday & "' AND ([Ending Date] >= '" & sToday & "' OR [Ending Date] = '') AND [Brand No_] = '' AND [Menu No_] = '' AND [Item No_] = ''"

            Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        End Function

        Sub UpdateAllLinkItem()
            Dim SQL As String
            Dim sItemNo As String, sName As String, sLinkURL As String
            SQL = " SELECT No_, Name FROM Item WHERE RTRIM(ISNULL([Link URL],'')) = '' "
            Dim tbl As DataTable
            tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
            Dim sConnectionString As String = GetConnectString()
            Dim nIJ As Integer
            For nIJ = 0 To tbl.Rows.Count - 1
                sItemNo = tbl.Rows(nIJ).Item("No_")
                sName = tbl.Rows(nIJ).Item("Name").ToString.Trim
                sLinkURL = ConvertIntoNone(sName)
                SQL = " UPDATE Item SET [Link URL] = '" & sLinkURL & "' WHERE No_ = '" & sItemNo & "'"
                DBHelper.ExecuteNonQuery(sConnectionString, CommandType.Text, SQL)
            Next
        End Sub

        Function GetDescription(ByVal sMenuCategoryNo As String, ByVal sItemNo As String) As String
            Dim SQL As String, sHtml As String = "", nIJ As Integer
            Dim tbl As DataTable
            SQL = " SELECT F.[Feature Name], F.[Feature Value], C.[Unit Of Measure] " & _
                   " FROM [Item Features] F " & _
                   " LEFT JOIN [Categoy Feature] C ON F.[Feature Group No_] = C.[Feature Group No_] AND F.[Feature No_] = C.[Feature No_] AND C.[Category No_] = '" & sMenuCategoryNo & "' " & _
                   " WHERE F.[Item No_]  = '" & sItemNo & "' AND C.[Show In List] = 1 AND RTRIM(F.[Feature Value]) <> '' "
            tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
            For nIJ = 0 To tbl.Rows.Count - 1
                sHtml &= tbl.Rows(nIJ).Item("Feature Name") & ": " & tbl.Rows(nIJ).Item("Feature Value") & " " & tbl.Rows(nIJ).Item("Unit Of Measure") & "<br />"
            Next
            Return sHtml
        End Function

        Function ShowFlash(ByVal sFlashFile As String) As String
            Dim sHtml As String = ""
            sHtml &= " <object width=""400"" height=""450"" type=""application/x-shockwave-flash"" data=""" & GetUrl() & "/media/" & sFlashFile & """ id=""obj_360""> " & vbCrLf & _
                    " <param value=""" & GetUrl() & "/media/" & sFlashFile & """ name=""src"" id=""param_360"">" & vbCrLf & _
                    " </object>"
            Return sHtml
        End Function

        Function GetMainColorItemNo(ByVal sItemNo As String) As String
            Dim SQL As String = "", sOriginItemNo As String
            SQL = " SELECT [Item Origin No_] FROM [Item Color] WHERE [Item No_] = '" & sItemNo & "'"
            sOriginItemNo = ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), "").ToString.Trim
            Return sOriginItemNo
        End Function


        Function showCustomerRating(ByVal sNo As String) As String
            Dim sHtml As String, nNumOfReview As Integer
            Dim objCustomerReview As New adv.Business.CustomerReview
            Dim sColorItemNo As String = ""
            sColorItemNo = GetMainColorItemNo(sNo)
            If sColorItemNo.Trim <> "" And sColorItemNo <> sNo Then sNo = sColorItemNo
            sHtml = ShowRating(objCustomerReview.GetRating(sNo))
            nNumOfReview = objCustomerReview.GetNumOfRating(sNo)
            If nNumOfReview = 0 Then
                sHtml &= "(0)"
            Else
                sHtml &= "(" & nNumOfReview & ")"
            End If
            Return sHtml
        End Function

        Function GetVariants(ByVal sNo As String) As DataTable
            Dim SQL As String
            SQL = " SELECT [Variant No_], [Variant Description] FROM [Item Variants] WHERE [Item No_] = '" & sNo & "' "
            Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        End Function

        Function ExistInOrder(ByVal sNo As String) As Boolean
            Dim SQL As String
            SQL = " SELECT TOP 1 [Document No_] FROM [Sales Line] WHERE [Item No_] = '" & sNo & "'"
            Return ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), "").ToString.Trim <> ""
        End Function

        Function DeleteItem(ByVal sNo As String) As Boolean
            Dim SQL As String
            Try
                SQL = " DELETE FROM Item WHERE No_ = '" & sNo & "'"
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
                SQL = " DELETE FROM [Item Features] WHERE [Item No_] = '" & sNo & "'"
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Shared Function LoadProductssListWithSubCategoryById(ByVal pageNumber As Integer, ByVal rowNumber As Integer, ByRef categoryId As String, ByVal BrandName As String, ByVal pfrom As Integer, ByVal pto As Integer, ByRef toalRow As Integer) As DataTable
            Dim arParams() As OleDbParameter = New OleDbParameter(6) {}
            Try
                arParams(0) = DBHelper.createParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber)
                arParams(1) = DBHelper.createParameter("@RowsPage", SqlDbType.Int, ParameterDirection.Input, rowNumber)
                arParams(2) = DBHelper.createParameter("@CategoryId", SqlDbType.VarChar, ParameterDirection.Input, categoryId)
                arParams(3) = DBHelper.createParameter("@BrandName", SqlDbType.VarChar, ParameterDirection.Input, BrandName)
                arParams(4) = DBHelper.createParameter("@PriceFrom", SqlDbType.VarChar, ParameterDirection.Input, pfrom)
                arParams(5) = DBHelper.createParameter("@PriceTo", SqlDbType.VarChar, ParameterDirection.Input, pto)

                arParams(6) = DBHelper.createParameter("@toalRow", SqlDbType.Int, ParameterDirection.Output, toalRow)

                'Dim sReader As IDataReader@BrandName
                'sReader = DBHelper.ExecuteReader(GetConnectString, CommandType.StoredProcedure, "GetProductsList", arParams)
                'SqlHelper.CreateCommand(GetConnectString, arParams)
                Dim cmd As New OleDbCommand
                cmd.CommandText = "GetProductsListWithSubCategory"
                cmd.CommandType = CommandType.StoredProcedure
                Dim con As OleDbConnection
                con = New OleDbConnection(GetConnectString())
                cmd.Connection = con
                AttachParameters(cmd, arParams)
                If con.State.ToString <> "Open" Then
                    con.Open()
                End If
                'cmd.ExecuteNonQuery()
                Dim dataRed As OleDbDataReader = cmd.ExecuteReader
                Dim tbl As DataTable = New DataTable
                tbl.Load(dataRed)
                toalRow = cmd.Parameters("@toalRow").Value
                'toalRow = 10
                con.Close()
                Return tbl
            Catch ex As Exception
                Return New DataTable
            End Try
        End Function

        Public Shared Function SearchItems(ByVal pageNumber As Integer, ByVal rowNumber As Integer, ByRef categoryId As String, ByVal keywork As String, ByRef toalRow As Integer) As DataTable
            Dim arParams() As OleDbParameter = New OleDbParameter(6) {}
            Try
                arParams(0) = DBHelper.createParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber)
                arParams(1) = DBHelper.createParameter("@RowsPage", SqlDbType.Int, ParameterDirection.Input, rowNumber)
                arParams(2) = DBHelper.createParameter("@CategoryId", SqlDbType.VarChar, ParameterDirection.Input, categoryId)
                arParams(3) = DBHelper.createParameter("@KeyWord", SqlDbType.VarChar, ParameterDirection.Input, keywork)
                arParams(6) = DBHelper.createParameter("@toalRow", SqlDbType.Int, ParameterDirection.Output, toalRow)

                'Dim sReader As IDataReader@BrandName
                'sReader = DBHelper.ExecuteReader(GetConnectString, CommandType.StoredProcedure, "GetProductsList", arParams)
                'SqlHelper.CreateCommand(GetConnectString, arParams)
                Dim cmd As New OleDbCommand
                cmd.CommandText = "SearchProductsList"
                cmd.CommandType = CommandType.StoredProcedure
                Dim con As OleDbConnection
                con = New OleDbConnection(GetConnectString())
                cmd.Connection = con
                AttachParameters(cmd, arParams)
                If con.State.ToString <> "Open" Then
                    con.Open()
                End If
                'cmd.ExecuteNonQuery()
                Dim dataRed As OleDbDataReader = cmd.ExecuteReader
                Dim tbl As DataTable = New DataTable
                tbl.Load(dataRed)
                toalRow = cmd.Parameters("@toalRow").Value
                'toalRow = 10
                con.Close()
                Return tbl
            Catch ex As Exception
                Return New DataTable
            End Try
        End Function

        Public Shared Function LoadSpecialProductsList(ByVal pageNumber As Integer, ByVal rowNumber As Integer, ByRef stypeId As Integer, ByRef toalRow As Integer) As DataTable
            Dim arParams() As OleDbParameter = New OleDbParameter(3) {}
            Try
                arParams(0) = DBHelper.createParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber)
                arParams(1) = DBHelper.createParameter("@RowsPage", SqlDbType.Int, ParameterDirection.Input, rowNumber)
                arParams(2) = DBHelper.createParameter("@stypeId", SqlDbType.Int, ParameterDirection.Input, stypeId)

                arParams(3) = DBHelper.createParameter("@toalRow", SqlDbType.Int, ParameterDirection.Output, toalRow)

                'Dim sReader As IDataReader@BrandName
                'sReader = DBHelper.ExecuteReader(GetConnectString, CommandType.StoredProcedure, "GetProductsList", arParams)
                'SqlHelper.CreateCommand(GetConnectString, arParams)
                Dim cmd As New OleDbCommand
                cmd.CommandText = "LoadSpecialProductsList"
                cmd.CommandType = CommandType.StoredProcedure
                Dim con As OleDbConnection
                con = New OleDbConnection(GetConnectString())
                cmd.Connection = con
                AttachParameters(cmd, arParams)
                If con.State.ToString <> "Open" Then
                    con.Open()
                End If
                'cmd.ExecuteNonQuery()
                Dim dataRed As OleDbDataReader = cmd.ExecuteReader
                Dim tbl As DataTable = New DataTable
                tbl.Load(dataRed)
                toalRow = cmd.Parameters("@toalRow").Value
                'toalRow = 10
                con.Close()
                Return tbl
            Catch ex As Exception
                Return New DataTable
            End Try
        End Function

        Public Shared Function LoadProductssListById(ByVal pageNumber As Integer, ByVal rowNumber As Integer, ByRef categoryId As String, ByVal BrandName As String, ByVal pfrom As Integer, ByVal pto As Integer, ByRef toalRow As Integer) As DataTable
            Dim arParams() As OleDbParameter = New OleDbParameter(6) {}
            Try

                arParams(0) = DBHelper.createParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber)
                arParams(1) = DBHelper.createParameter("@RowsPage", SqlDbType.Int, ParameterDirection.Input, rowNumber)
                arParams(2) = DBHelper.createParameter("@CategoryId", SqlDbType.VarChar, ParameterDirection.Input, categoryId)
                arParams(3) = DBHelper.createParameter("@BrandName", SqlDbType.VarChar, ParameterDirection.Input, BrandName)
                arParams(4) = DBHelper.createParameter("@PriceFrom", SqlDbType.VarChar, ParameterDirection.Input, pfrom)
                arParams(5) = DBHelper.createParameter("@PriceTo", SqlDbType.VarChar, ParameterDirection.Input, pto)

                arParams(6) = DBHelper.createParameter("@toalRow", SqlDbType.Int, ParameterDirection.Output, toalRow)

                'Dim sReader As IDataReader@BrandName
                'sReader = DBHelper.ExecuteReader(GetConnectString, CommandType.StoredProcedure, "GetProductsList", arParams)
                'SqlHelper.CreateCommand(GetConnectString, arParams)
                Dim cmd As New OleDbCommand
                cmd.CommandText = "GetProductsList"
                cmd.CommandType = CommandType.StoredProcedure
                Dim con As OleDbConnection
                con = New OleDbConnection(GetConnectString())
                cmd.Connection = con
                AttachParameters(cmd, arParams)
                If con.State.ToString <> "Open" Then
                    con.Open()
                End If
                'cmd.ExecuteNonQuery()
                Dim dataRed As OleDbDataReader = cmd.ExecuteReader
                Dim tbl As DataTable = New DataTable
                tbl.Load(dataRed)
                toalRow = cmd.Parameters("@toalRow").Value
                'toalRow = 10
                con.Close()
                Return tbl
            Catch ex As Exception
                Return New DataTable
            End Try
        End Function
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
#End Region

    End Class
End Namespace