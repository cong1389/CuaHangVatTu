Imports Microsoft.VisualBasic
Imports System.Data

Namespace adv.Business
    Public Class CardDataEntry
#Region "Khai báo"
        Private mEntryCode As String = ""
        Private mEntryType As Integer = 0
        Private mDiscountType As Integer = 0
        Private mDescriptions As String = ""
        Private mAmount As Double = 0
        Private mMaxAmount As Double = 0
        Private mStartingDate As String = ""
        Private mEndingDate As String = ""
        Private mPaymentType As Integer = 0
        Private mApplied As Integer = 0
        Private mAppliedAmount As Double = 0
        Private mOrderNo_ As String = ""
        Private mApplyForCode As String = ""
        Private mPromotionNo_ As String = ""
        Private mBillAmountFrom As Double = 0
        Private mBillAmountTo As Double = 0
        Private mMultiApplied As Integer = 0

        Property EntryCode() As String
            Get
                Return mEntryCode
            End Get
            Set(ByVal value As String)
                mEntryCode = value
            End Set
        End Property
        Property EntryType() As Integer
            Get
                Return mEntryType
            End Get
            Set(ByVal value As Integer)
                mEntryType = value
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
        Property Descriptions() As String
            Get
                Return mDescriptions
            End Get
            Set(ByVal value As String)
                mDescriptions = value
            End Set
        End Property
        Property Amount() As Double
            Get
                Return mAmount
            End Get
            Set(ByVal value As Double)
                mAmount = value
            End Set
        End Property
        Property MaxAmount() As Double
            Get
                Return mMaxAmount
            End Get
            Set(ByVal value As Double)
                mMaxAmount = value
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
        Property PaymentType() As Integer
            Get
                Return mPaymentType
            End Get
            Set(ByVal value As Integer)
                mPaymentType = value
            End Set
        End Property
        Property Applied() As Integer
            Get
                Return mApplied
            End Get
            Set(ByVal value As Integer)
                mApplied = value
            End Set
        End Property
        Property AppliedAmount() As Double
            Get
                Return mAppliedAmount
            End Get
            Set(ByVal value As Double)
                mAppliedAmount = value
            End Set
        End Property
        Property OrderNo_() As String
            Get
                Return mOrderNo_
            End Get
            Set(ByVal value As String)
                mOrderNo_ = value
            End Set
        End Property
        Property ApplyForCode() As String
            Get
                Return mApplyForCode
            End Get
            Set(ByVal value As String)
                mApplyForCode = value
            End Set
        End Property
        Property PromotionNo_() As String
            Get
                Return mPromotionNo_
            End Get
            Set(ByVal value As String)
                mPromotionNo_ = value
            End Set
        End Property
        Property BillAmountFrom() As Double
            Get
                Return mBillAmountFrom
            End Get
            Set(ByVal value As Double)
                mBillAmountFrom = value
            End Set
        End Property
        Property BillAmountTo() As Double
            Get
                Return mBillAmountTo
            End Get
            Set(ByVal value As Double)
                mBillAmountTo = value
            End Set
        End Property
        Property MultiApplied() As Integer
            Get
                Return mMultiApplied
            End Get
            Set(ByVal value As Integer)
                mMultiApplied = value
            End Set
        End Property

#End Region
#Region "Function"
        Public Function Load(ByVal sEntryCode As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Card Data Entry] WHERE [Entry Code] = '" & sEntryCode & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mEntryCode = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mEntryType = GetValue(SqlReader, 1, typeOfColumn.GetInt32)
                    mDiscountType = GetValue(SqlReader, 2, typeOfColumn.GetInt32)
                    mDescriptions = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mAmount = GetValue(SqlReader, 4, typeOfColumn.GetDecimal)
                    mMaxAmount = GetValue(SqlReader, 5, typeOfColumn.GetDecimal)
                    mStartingDate = GetValue(SqlReader, 6, typeOfColumn.GetString)
                    mEndingDate = GetValue(SqlReader, 7, typeOfColumn.GetString)
                    mPaymentType = GetValue(SqlReader, 8, typeOfColumn.GetInt32)
                    mApplied = GetValue(SqlReader, 9, typeOfColumn.GetInt32)
                    mAppliedAmount = GetValue(SqlReader, 10, typeOfColumn.GetDecimal)
                    mOrderNo_ = GetValue(SqlReader, 11, typeOfColumn.GetString)
                    mApplyForCode = GetValue(SqlReader, 12, typeOfColumn.GetString)
                    mPromotionNo_ = GetValue(SqlReader, 13, typeOfColumn.GetString)
                    mBillAmountFrom = GetValue(SqlReader, 14, typeOfColumn.GetDecimal)
                    mBillAmountTo = GetValue(SqlReader, 15, typeOfColumn.GetDecimal)
                    mMultiApplied = GetValue(SqlReader, 16, typeOfColumn.GetInt32)
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
                arParams(0) = DBHelper.createParameter("@EntryCode", SqlDbType.NVarChar, ParameterDirection.Input, mEntryCode)
                arParams(1) = DBHelper.createParameter("@EntryType", SqlDbType.Int, ParameterDirection.Input, mEntryType)
                arParams(2) = DBHelper.createParameter("@DiscountType", SqlDbType.Int, ParameterDirection.Input, mDiscountType)
                arParams(3) = DBHelper.createParameter("@Descriptions", SqlDbType.NVarChar, ParameterDirection.Input, mDescriptions)
                arParams(4) = DBHelper.createParameter("@Amount", SqlDbType.Decimal, ParameterDirection.Input, mAmount)
                arParams(5) = DBHelper.createParameter("@MaxAmount", SqlDbType.Decimal, ParameterDirection.Input, mMaxAmount)
                arParams(6) = DBHelper.createParameter("@StartingDate", SqlDbType.NVarChar, ParameterDirection.Input, mStartingDate)
                arParams(7) = DBHelper.createParameter("@EndingDate", SqlDbType.NVarChar, ParameterDirection.Input, mEndingDate)
                arParams(8) = DBHelper.createParameter("@PaymentType1", SqlDbType.Int, ParameterDirection.Input, mPaymentType)
                arParams(9) = DBHelper.createParameter("@Applied", SqlDbType.Int, ParameterDirection.Input, mApplied)
                arParams(10) = DBHelper.createParameter("@AppliedAmount", SqlDbType.Decimal, ParameterDirection.Input, mAppliedAmount)
                arParams(11) = DBHelper.createParameter("@OrderNo_", SqlDbType.NVarChar, ParameterDirection.Input, mOrderNo_)
                arParams(12) = DBHelper.createParameter("@ApplyForCode", SqlDbType.NVarChar, ParameterDirection.Input, mApplyForCode)
                arParams(13) = DBHelper.createParameter("@PromotionNo_", SqlDbType.NVarChar, ParameterDirection.Input, mPromotionNo_)
                arParams(14) = DBHelper.createParameter("@BillAmountFrom", SqlDbType.Decimal, ParameterDirection.Input, mBillAmountFrom)
                arParams(15) = DBHelper.createParameter("@BillAmountTo", SqlDbType.Decimal, ParameterDirection.Input, mBillAmountTo)
                arParams(16) = DBHelper.createParameter("@MultiApplied", SqlDbType.Int, ParameterDirection.Input, mMultiApplied)
                DBHelper.ExecuteNonQuery(GetUrl, CommandType.StoredProcedure, "SP_CardDataEntry", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
#End Region

    End Class

End Namespace