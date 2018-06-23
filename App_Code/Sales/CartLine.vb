Imports Microsoft.VisualBasic
Imports System.Data

Namespace adv.Business
    Public Class CartLine
#Region "Khai báo"
        Private mCartNo_ As String = ""
        Private mLineNo_ As Integer = 0
        Private mItemNo_ As String = ""
        Private mDescriptions As String = ""
        Private mQuantity As Integer = 0
        Private mUnitPrice As Double = 0
        Private mAmountIncVAT As Double = 0
        Private mOrderDate As String = ""
        Private mCustomerIP As String = ""
        Private mVariants As String = ""
        Private mUnitOrMeasure As String = ""
        Private mLineType As Integer = 0
        Private mDiscountPercent As Double = 0
        Private mDiscountAmount As Double = 0
        Private mPromotionNo_ As String = ""
        Property CartNo_() As String
            Get
                Return mCartNo_
            End Get
            Set(ByVal value As String)
                mCartNo_ = value
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
        Property Descriptions() As String
            Get
                Return mDescriptions
            End Get
            Set(ByVal value As String)
                mDescriptions = value
            End Set
        End Property
        Property Quantity() As Integer
            Get
                Return mQuantity
            End Get
            Set(ByVal value As Integer)
                mQuantity = value
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
        Property AmountIncVAT() As Double
            Get
                Return mAmountIncVAT
            End Get
            Set(ByVal value As Double)
                mAmountIncVAT = value
            End Set
        End Property
        Property OrderDate() As String
            Get
                Return mOrderDate
            End Get
            Set(ByVal value As String)
                mOrderDate = value
            End Set
        End Property
        Property CustomerIP() As String
            Get
                Return mCustomerIP
            End Get
            Set(ByVal value As String)
                mCustomerIP = value
            End Set
        End Property
        Property Variants As String
            Get
                Return mVariants
            End Get
            Set(ByVal value As String)
                mVariants = value
            End Set
        End Property
        Property UnitOrMeasure As String
            Get
                Return mUnitOrMeasure
            End Get
            Set(ByVal value As String)
                mUnitOrMeasure = value
            End Set
        End Property
        Property LineType As Integer
            Get
                Return mLineType
            End Get
            Set(ByVal value As Integer)
                mLineType = value
            End Set
        End Property
        Property DiscountPercent As Double
            Get
                Return mDiscountPercent
            End Get
            Set(ByVal value As Double)
                mDiscountPercent = value
            End Set
        End Property
        Property DiscountAmount As Double
            Get
                Return mDiscountAmount
            End Get
            Set(ByVal value As Double)
                mDiscountAmount = value
            End Set
        End Property
        Property PromotionNo As String
            Get
                Return mPromotionNo_
            End Get
            Set(ByVal value As String)
                mPromotionNo_ = value
            End Set
        End Property
#End Region
#Region "Function"
   
        Public Function Save() As Boolean
            Dim arParams() As IDataParameter = New IDataParameter(14) {}
            Try
                arParams(0) = DBHelper.createParameter("@CartNo_", SqlDbType.NVarChar, ParameterDirection.Input, mCartNo_)
                arParams(1) = DBHelper.createParameter("@LineNo_", SqlDbType.Int, ParameterDirection.Input, mLineNo_)
                arParams(2) = DBHelper.createParameter("@ItemNo_", SqlDbType.NVarChar, ParameterDirection.Input, mItemNo_)
                arParams(3) = DBHelper.createParameter("@Descriptions", SqlDbType.NVarChar, ParameterDirection.Input, mDescriptions)
                arParams(4) = DBHelper.createParameter("@Quantity", SqlDbType.Int, ParameterDirection.Input, mQuantity)
                arParams(5) = DBHelper.createParameter("@UnitPrice", SqlDbType.Decimal, ParameterDirection.Input, mUnitPrice)
                arParams(6) = DBHelper.createParameter("@AmountIncVAT", SqlDbType.Decimal, ParameterDirection.Input, mAmountIncVAT)
                arParams(7) = DBHelper.createParameter("@OrderDate", SqlDbType.NVarChar, ParameterDirection.Input, mOrderDate)
                arParams(8) = DBHelper.createParameter("@CustomerIP", SqlDbType.NVarChar, ParameterDirection.Input, mCustomerIP)
                arParams(9) = DBHelper.createParameter("@Variants", SqlDbType.NVarChar, ParameterDirection.Input, mVariants)
                arParams(10) = DBHelper.createParameter("@UnitOfMeasure", SqlDbType.NVarChar, ParameterDirection.Input, mUnitOrMeasure)
                arParams(11) = DBHelper.createParameter("@LineType", SqlDbType.Int, ParameterDirection.Input, mLineType)
                arParams(12) = DBHelper.createParameter("@DiscountPercent", SqlDbType.Decimal, ParameterDirection.Input, mDiscountPercent)
                arParams(13) = DBHelper.createParameter("@DiscountAmount", SqlDbType.Decimal, ParameterDirection.Input, mDiscountAmount)
                arParams(14) = DBHelper.createParameter("@PromotionNo_", SqlDbType.NVarChar, ParameterDirection.Input, mPromotionNo_)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_CartLine", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
#End Region

    End Class
End Namespace