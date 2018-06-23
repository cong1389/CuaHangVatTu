Imports Microsoft.VisualBasic
Imports System.Data

Namespace adv.Business
    Public Class PaymentEntry
#Region "Khai báo"
        Private mOrderNo_ As String = ""
        Private mLineNo_ As Integer = 0
        Private mOrderDate As String = ""
        Private mTenderType As String = ""
        Private mAmount As Double = 0
        Private mCardNo_ As String = ""
        Private mCartNo_ As String = ""
        Private mCustomerNo_ As String = ""
        Private mDescriptions As String = ""
        Private mPaymentType As Integer = 0

        Property OrderNo_() As String
            Get
                Return mOrderNo_
            End Get
            Set(ByVal value As String)
                mOrderNo_ = value
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
        Property OrderDate() As String
            Get
                Return mOrderDate
            End Get
            Set(ByVal value As String)
                mOrderDate = value
            End Set
        End Property
        Property TenderType() As String
            Get
                Return mTenderType
            End Get
            Set(ByVal value As String)
                mTenderType = value
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
        Property CardNo_() As String
            Get
                Return mCardNo_
            End Get
            Set(ByVal value As String)
                mCardNo_ = value
            End Set
        End Property
        Property CartNo_ As String
            Get
                Return mCartNo_
            End Get
            Set(ByVal value As String)
                mCartNo_ = value
            End Set
        End Property
        Property CustomerNo_ As String
            Get
                Return mCustomerNo_
            End Get
            Set(ByVal value As String)
                mCustomerNo_ = value
            End Set
        End Property
        Property Descriptions As String
            Get
                Return mDescriptions
            End Get
            Set(ByVal value As String)
                mDescriptions = value
            End Set
        End Property
        Property PaymentType As Integer
            Get
                Return mPaymentType
            End Get
            Set(ByVal value As Integer)
                mPaymentType = value
            End Set
        End Property
#End Region
#Region "Function"

        Public Function Save() As Boolean
            Dim arParams() As IDataParameter = New IDataParameter(9) {}
            Try
                arParams(0) = DBHelper.createParameter("@OrderNo_", SqlDbType.NVarChar, ParameterDirection.Input, mOrderNo_)
                arParams(1) = DBHelper.createParameter("@LineNo_", SqlDbType.Int, ParameterDirection.Input, mLineNo_)
                arParams(2) = DBHelper.createParameter("@OrderDate", SqlDbType.NVarChar, ParameterDirection.Input, mOrderDate)
                arParams(3) = DBHelper.createParameter("@TenderType", SqlDbType.NVarChar, ParameterDirection.Input, mTenderType)
                arParams(4) = DBHelper.createParameter("@Amount", SqlDbType.Decimal, ParameterDirection.Input, mAmount)
                arParams(5) = DBHelper.createParameter("@CardNo_", SqlDbType.NVarChar, ParameterDirection.Input, mCardNo_)
                arParams(6) = DBHelper.createParameter("@CartNo_", SqlDbType.NVarChar, ParameterDirection.Input, mCartNo_)
                arParams(7) = DBHelper.createParameter("@CustomerNo_", SqlDbType.NVarChar, ParameterDirection.Input, mCustomerNo_)
                arParams(8) = DBHelper.createParameter("@Descriptions", SqlDbType.NVarChar, ParameterDirection.Input, mDescriptions)
                arParams(9) = DBHelper.createParameter("@PaymentType", SqlDbType.Int, ParameterDirection.Input, mPaymentType)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_PaymentEntry", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function Del(ByVal sOrderNo As String, ByVal nLineNo As Integer) As Boolean
            Dim arParams() As IDataParameter = New IDataParameter(1) {}
            Try
                arParams(0) = DBHelper.createParameter("@OrderNo_", SqlDbType.NVarChar, ParameterDirection.Input, sOrderNo)
                arParams(1) = DBHelper.createParameter("@LineNo_", SqlDbType.Int, ParameterDirection.Input, nLineNo)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_PaymentEntry_Del", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
#End Region

    End Class

End Namespace