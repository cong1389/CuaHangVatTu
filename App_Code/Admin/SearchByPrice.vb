Imports Microsoft.VisualBasic
Imports System.Data

Namespace adv.Business
    Public Class SearchByPrice
#Region "Khai báo"
        Private mLineNo_ As Integer = 0
        Private mCategoryNo_ As String = ""
        Private mDescriptions As String = ""
        Private mFromAmount As Double = 0
        Private mToAmount As Double = 0
        Private mOrderPosition As Integer = 0

        Property LineNo_() As Integer
            Get
                Return mLineNo_
            End Get
            Set(ByVal value As Integer)
                mLineNo_ = value
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
        Property Descriptions() As String
            Get
                Return mDescriptions
            End Get
            Set(ByVal value As String)
                mDescriptions = value
            End Set
        End Property
        Property FromAmount() As Double
            Get
                Return mFromAmount
            End Get
            Set(ByVal value As Double)
                mFromAmount = value
            End Set
        End Property
        Property ToAmount() As Double
            Get
                Return mToAmount
            End Get
            Set(ByVal value As Double)
                mToAmount = value
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

#End Region
#Region "Function"

        Public Function Save() As Boolean
            Dim arParams() As IDataParameter = New IDataParameter(5) {}
            Try
                arParams(0) = DBHelper.createParameter("@LineNo_", SqlDbType.Int, ParameterDirection.Input, mLineNo_)
                arParams(1) = DBHelper.createParameter("@CategoryNo_", SqlDbType.NVarChar, ParameterDirection.Input, mCategoryNo_)
                arParams(2) = DBHelper.createParameter("@Descriptions", SqlDbType.NVarChar, ParameterDirection.Input, mDescriptions)
                arParams(3) = DBHelper.createParameter("@FromAmount", SqlDbType.Decimal, ParameterDirection.Input, mFromAmount)
                arParams(4) = DBHelper.createParameter("@ToAmount", SqlDbType.Decimal, ParameterDirection.Input, mToAmount)
                arParams(5) = DBHelper.createParameter("@OrderPosition", SqlDbType.Int, ParameterDirection.Input, mOrderPosition)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_SearchProductByPrice", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function


#End Region

    End Class
End Namespace
