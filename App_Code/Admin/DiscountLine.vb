Imports System.Data
Imports Microsoft.VisualBasic

Namespace adv.Business
    Public Class DiscountLine
#Region "Khai báo"
        Private mDiscountNo_ As String = ""
        Private mDescription As String = ""
        Private mPriceGroup As String = ""
        Private mStoreGroup As String = ""
        Private mLineNo_ As Integer = 0
        Private mItemNo_ As String = ""
        Private mTriggerDiscount As Integer = 0
        Private mDisc_Type As Integer = 0
        Private mOriginPrice As Double = 0
        Private mDealPriceValue As Double = 0
        Private mStartingDate As String = ""
        Private mEndingDate As String = ""
        Private mDiscountType As Integer = 0
        Private mQuantity As Integer = 0
        Private mUnitOfMeasure As String = ""

        Property DiscountNo_() As String
            Get
                Return mDiscountNo_
            End Get
            Set(ByVal value As String)
                mDiscountNo_ = value
            End Set
        End Property
        Property Description() As String
            Get
                Return mDescription
            End Get
            Set(ByVal value As String)
                mDescription = value
            End Set
        End Property
        Property PriceGroup() As String
            Get
                Return mPriceGroup
            End Get
            Set(ByVal value As String)
                mPriceGroup = value
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
        Property LineNo_() As Integer
            Get
                Return mLineNo_
            End Get
            Set(ByVal value As Integer)
                mLineNo_ = value
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
        Property TriggerDiscount() As Integer
            Get
                Return mTriggerDiscount
            End Get
            Set(ByVal value As Integer)
                mTriggerDiscount = value
            End Set
        End Property
        Property Disc_Type() As Integer
            Get
                Return mDisc_Type
            End Get
            Set(ByVal value As Integer)
                mDisc_Type = value
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
        Property DealPriceValue() As Double
            Get
                Return mDealPriceValue
            End Get
            Set(ByVal value As Double)
                mDealPriceValue = value
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
        Property DiscountType() As Integer
            Get
                Return mDiscountType
            End Get
            Set(ByVal value As Integer)
                mDiscountType = value
            End Set
        End Property
        Property Quantity As Integer
            Get
                Return mQuantity
            End Get
            Set(ByVal value As Integer)
                mQuantity = value
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
#End Region
#Region "Function"
        Public Function Load(ByVal sDiscountNo As String, ByVal nLineNo As Integer) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Discount Line] WHERE [Discount No_] = '" & sDiscountNo & "' AND [Line No_] = '" & nLineNo & "'"
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mDiscountNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mDescription = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mPriceGroup = GetValue(SqlReader, 2, typeOfColumn.GetString)
                    mStoreGroup = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mLineNo_ = GetValue(SqlReader, 4, typeOfColumn.GetInt32)
                    mItemNo_ = GetValue(SqlReader, 5, typeOfColumn.GetString)
                    mTriggerDiscount = GetValue(SqlReader, 6, typeOfColumn.GetInt32)
                    mDisc_Type = GetValue(SqlReader, 7, typeOfColumn.GetInt32)
                    mOriginPrice = GetValue(SqlReader, 8, typeOfColumn.GetDecimal)
                    mDealPriceValue = GetValue(SqlReader, 9, typeOfColumn.GetDecimal)
                    mStartingDate = GetValue(SqlReader, 10, typeOfColumn.GetString)
                    mEndingDate = GetValue(SqlReader, 11, typeOfColumn.GetString)
                    mDiscountType = GetValue(SqlReader, 12, typeOfColumn.GetInt32)
                    mQuantity = GetValue(SqlReader, 13, typeOfColumn.GetInt32)
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
            Dim arParams() As IDataParameter = New IDataParameter(13) {}
            Try
                arParams(0) = DBHelper.createParameter("@DiscountNo_", SqlDbType.NVarChar, ParameterDirection.Input, mDiscountNo_)
                arParams(1) = DBHelper.createParameter("@Description", SqlDbType.NVarChar, ParameterDirection.Input, mDescription)
                arParams(2) = DBHelper.createParameter("@PriceGroup", SqlDbType.NVarChar, ParameterDirection.Input, mPriceGroup)
                arParams(3) = DBHelper.createParameter("@StoreGroup", SqlDbType.NVarChar, ParameterDirection.Input, mStoreGroup)
                arParams(4) = DBHelper.createParameter("@LineNo_", SqlDbType.Int, ParameterDirection.Input, mLineNo_)
                arParams(5) = DBHelper.createParameter("@ItemNo_", SqlDbType.NVarChar, ParameterDirection.Input, mItemNo_)
                arParams(6) = DBHelper.createParameter("@TriggerDiscount", SqlDbType.Int, ParameterDirection.Input, mTriggerDiscount)
                arParams(7) = DBHelper.createParameter("@Disc_Type", SqlDbType.Int, ParameterDirection.Input, mDisc_Type)
                arParams(8) = DBHelper.createParameter("@OriginPrice", SqlDbType.Decimal, ParameterDirection.Input, mOriginPrice)
                arParams(9) = DBHelper.createParameter("@DealPriceValue", SqlDbType.Decimal, ParameterDirection.Input, mDealPriceValue)
                arParams(10) = DBHelper.createParameter("@StartingDate", SqlDbType.NVarChar, ParameterDirection.Input, mStartingDate)
                arParams(11) = DBHelper.createParameter("@EndingDate", SqlDbType.NVarChar, ParameterDirection.Input, mEndingDate)
                arParams(12) = DBHelper.createParameter("@DiscountType", SqlDbType.Int, ParameterDirection.Input, mDiscountType)
                arParams(13) = DBHelper.createParameter("@Quantity", SqlDbType.Int, ParameterDirection.Input, mQuantity)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_DiscountLine", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
#End Region

    End Class
End Namespace