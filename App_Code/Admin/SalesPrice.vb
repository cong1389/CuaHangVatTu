Imports Microsoft.VisualBasic
Imports System.Data

Namespace adv.Business
    Public Class SalesPrice
#Region "Khai báo"
        Private mItemNo_ As String = ""
        Private mStoreGroup As String = ""
        Private mSalesPriceNo_ As String = ""
        Private mUnitOfMeasureNo_ As String = ""
        Private mStartingDate As String = ""
        Private mEndingDate As String = ""
        Private mUnitPrice As Double = 0
        Private mPriceIncVAT As Integer = 0
        Private mLineType As Integer = 0
        Private mGiftItemNo_ As String = ""
        Private mGiftDescription As String = ""
        Private mOriginPrice As Double = 0
        Private mDealPrice As Double = 0
        Private mQuantity As Double = 0
        Private mPeriodicDiscountNo_ As String = ""
        Private mLineNo_ As Integer = 0

        Property ItemNo_() As String
            Get
                Return mItemNo_
            End Get
            Set(ByVal value As String)
                mItemNo_ = value
            End Set
        End Property
        Property StoreGroup() As String
            Get
                Return mStoreGroup
            End Get
            Set(ByVal value As String)
                mStoreGroup = value
            End Set
        End Property
        Property SalesPriceNo_() As String
            Get
                Return mSalesPriceNo_
            End Get
            Set(ByVal value As String)
                mSalesPriceNo_ = value
            End Set
        End Property
        Property UnitOfMeasureNo_() As String
            Get
                Return mUnitOfMeasureNo_
            End Get
            Set(ByVal value As String)
                mUnitOfMeasureNo_ = value
            End Set
        End Property
        Property StartingDate() As String
            Get
                Return mStartingDate
            End Get
            Set(ByVal value As String)
                mStartingDate = value
            End Set
        End Property
        Property EndingDate() As String
            Get
                Return mEndingDate
            End Get
            Set(ByVal value As String)
                mEndingDate = value
            End Set
        End Property
        Property UnitPrice() As Double
            Get
                Return mUnitPrice
            End Get
            Set(ByVal value As Double)
                mUnitPrice = value
            End Set
        End Property
        Property PriceIncVAT() As Integer
            Get
                Return mPriceIncVAT
            End Get
            Set(ByVal value As Integer)
                mPriceIncVAT = value
            End Set
        End Property
        Property LineType() As Integer
            Get
                Return mLineType
            End Get
            Set(ByVal value As Integer)
                mLineType = value
            End Set
        End Property
        Property GiftItemNo_() As String
            Get
                Return mGiftItemNo_
            End Get
            Set(ByVal value As String)
                mGiftItemNo_ = value
            End Set
        End Property
        Property GiftDescription() As String
            Get
                Return mGiftDescription
            End Get
            Set(ByVal value As String)
                mGiftDescription = value
            End Set
        End Property
        Property OriginPrice() As Double
            Get
                Return mOriginPrice
            End Get
            Set(ByVal value As Double)
                mOriginPrice = value
            End Set
        End Property
        Property DealPrice() As Double
            Get
                Return mDealPrice
            End Get
            Set(ByVal value As Double)
                mDealPrice = value
            End Set
        End Property
        Property Quantity() As Double
            Get
                Return mQuantity
            End Get
            Set(ByVal value As Double)
                mQuantity = value
            End Set
        End Property
        Property PeriodicDiscountNo_() As String
            Get
                Return mPeriodicDiscountNo_
            End Get
            Set(ByVal value As String)
                mPeriodicDiscountNo_ = value
            End Set
        End Property
        Property LineNo As Integer
            Get
                Return mLineNo_
            End Get
            Set(ByVal value As Integer)
                mLineNo_ = value
            End Set
        End Property
#End Region
#Region "Function"
        Public Function Load(ByVal sItemNo_ As String, ByVal iLineNo_ As Integer) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Sales Price] WHERE [Item No_] = '" & sItemNo_ & "' AND [Line No_] = " & iLineNo_
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mItemNo_ = adv.Business.GetValue(SqlReader, 0, adv.Business.typeOfColumn.GetString)
                    mStoreGroup = adv.Business.GetValue(SqlReader, 1, adv.Business.typeOfColumn.GetString)
                    mSalesPriceNo_ = adv.Business.GetValue(SqlReader, 2, adv.Business.typeOfColumn.GetString)
                    mUnitOfMeasureNo_ = adv.Business.GetValue(SqlReader, 3, adv.Business.typeOfColumn.GetString)
                    mStartingDate = adv.Business.GetValue(SqlReader, 4, adv.Business.typeOfColumn.GetString)
                    mEndingDate = adv.Business.GetValue(SqlReader, 5, adv.Business.typeOfColumn.GetString)
                    mUnitPrice = adv.Business.GetValue(SqlReader, 6, adv.Business.typeOfColumn.GetDecimal)
                    mPriceIncVAT = adv.Business.GetValue(SqlReader, 7, adv.Business.typeOfColumn.GetInt32)
                    mLineType = adv.Business.GetValue(SqlReader, 8, adv.Business.typeOfColumn.GetInt32)
                    mGiftItemNo_ = adv.Business.GetValue(SqlReader, 9, adv.Business.typeOfColumn.GetString)
                    mGiftDescription = adv.Business.GetValue(SqlReader, 10, adv.Business.typeOfColumn.GetString)
                    mOriginPrice = adv.Business.GetValue(SqlReader, 11, adv.Business.typeOfColumn.GetDecimal)
                    mDealPrice = adv.Business.GetValue(SqlReader, 12, adv.Business.typeOfColumn.GetDecimal)
                    mQuantity = adv.Business.GetValue(SqlReader, 13, adv.Business.typeOfColumn.GetDecimal)
                    mPeriodicDiscountNo_ = adv.Business.GetValue(SqlReader, 14, adv.Business.typeOfColumn.GetString)
                    mLineNo_ = adv.Business.GetValue(SqlReader, 15, adv.Business.typeOfColumn.GetInt32)
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
            Dim arParams() As IDataParameter = New IDataParameter(16) {}
            Try
                arParams(1) = DBHelper.createParameter("@ItemNo_", SqlDbType.NVarChar, ParameterDirection.Input, mItemNo_)
                arParams(2) = DBHelper.createParameter("@StoreGroup", SqlDbType.NVarChar, ParameterDirection.Input, mStoreGroup)
                arParams(3) = DBHelper.createParameter("@SalesPriceNo_", SqlDbType.NVarChar, ParameterDirection.Input, mSalesPriceNo_)
                arParams(4) = DBHelper.createParameter("@UnitOfMeasureNo_", SqlDbType.NVarChar, ParameterDirection.Input, mUnitOfMeasureNo_)
                arParams(5) = DBHelper.createParameter("@StartingDate", SqlDbType.NVarChar, ParameterDirection.Input, mStartingDate)
                arParams(6) = DBHelper.createParameter("@EndingDate", SqlDbType.NVarChar, ParameterDirection.Input, mEndingDate)
                arParams(7) = DBHelper.createParameter("@UnitPrice", SqlDbType.Decimal, ParameterDirection.Input, mUnitPrice)
                arParams(8) = DBHelper.createParameter("@PriceIncVAT", SqlDbType.Int, ParameterDirection.Input, mPriceIncVAT)
                arParams(9) = DBHelper.createParameter("@LineType", SqlDbType.Int, ParameterDirection.Input, mLineType)
                arParams(10) = DBHelper.createParameter("@GiftItemNo_", SqlDbType.NVarChar, ParameterDirection.Input, mGiftItemNo_)
                arParams(11) = DBHelper.createParameter("@GiftDescription", SqlDbType.NVarChar, ParameterDirection.Input, mGiftDescription)
                arParams(12) = DBHelper.createParameter("@OriginPrice", SqlDbType.Decimal, ParameterDirection.Input, mOriginPrice)
                arParams(13) = DBHelper.createParameter("@DealPrice", SqlDbType.Decimal, ParameterDirection.Input, mDealPrice)
                arParams(14) = DBHelper.createParameter("@Quantity", SqlDbType.Decimal, ParameterDirection.Input, mQuantity)
                arParams(15) = DBHelper.createParameter("@PeriodicDiscountNo_", SqlDbType.NVarChar, ParameterDirection.Input, mPeriodicDiscountNo_)
                arParams(16) = DBHelper.createParameter("@LineNo_", SqlDbType.NVarChar, ParameterDirection.Input, mLineNo_)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_SalesPrice", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function


#End Region

    End Class
End Namespace