Imports Microsoft.VisualBasic
Imports System.Data

Namespace adv.Business
    Public Class SalesLine
#Region "Khai báo"
        Private mDocumentNo_ As String = ""
        Private mLineNo_ As Integer = 0
        Private mLineType As Integer = 0
        Private mItemNo_ As String = ""
        Private mItemName As String = ""
        Private mUnitofMeasure As String = ""
        Private mQuantity As Double = 0
        Private mUnitPriceIncVAT As Double = 0
        Private mDiscountAmount As Double = 0
        Private mDiscountPercent As Double = 0
        Private mVATPercent As Integer = 0
        Private mVATAmount As Double = 0
        Private mAmountIncVAT As Double = 0
        Private mDescriptions As String = ""

        Property DocumentNo_() As String
            Get
                Return mDocumentNo_
            End Get
            Set(ByVal value As String)
                mDocumentNo_ = value
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
        Property LineType() As Integer
            Get
                Return mLineType
            End Get
            Set(ByVal value As Integer)
                mLineType = value
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
        Property ItemName() As String
            Get
                Return mItemName
            End Get
            Set(ByVal value As String)
                mItemName = value
            End Set
        End Property
        Property UnitofMeasure() As String
            Get
                Return mUnitofMeasure
            End Get
            Set(ByVal value As String)
                mUnitofMeasure = value
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
        Property UnitPriceIncVAT() As Double
            Get
                Return mUnitPriceIncVAT
            End Get
            Set(ByVal value As Double)
                mUnitPriceIncVAT = value
            End Set
        End Property
        Property DiscountAmount() As Double
            Get
                Return mDiscountAmount
            End Get
            Set(ByVal value As Double)
                mDiscountAmount = value
            End Set
        End Property
        Property DiscountPercent() As Double
            Get
                Return mDiscountPercent
            End Get
            Set(ByVal value As Double)
                mDiscountPercent = value
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
        Property VATAmount() As Double
            Get
                Return mVATAmount
            End Get
            Set(ByVal value As Double)
                mVATAmount = value
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
        Property Descriptions() As String
            Get
                Return mDescriptions
            End Get
            Set(ByVal value As String)
                mDescriptions = value
            End Set
        End Property

#End Region
#Region "Function"
        Public Function Load(ByVal pKhoa As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Sales Line] WHERE Khoa = '" & pKhoa & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mDocumentNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mLineNo_ = GetValue(SqlReader, 1, typeOfColumn.GetInt32)
                    mLineType = GetValue(SqlReader, 2, typeOfColumn.GetInt32)
                    mItemNo_ = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mItemName = GetValue(SqlReader, 4, typeOfColumn.GetString)
                    mUnitofMeasure = GetValue(SqlReader, 5, typeOfColumn.GetString)
                    mQuantity = GetValue(SqlReader, 6, typeOfColumn.GetDecimal)
                    mUnitPriceIncVAT = GetValue(SqlReader, 7, typeOfColumn.GetDecimal)
                    mDiscountAmount = GetValue(SqlReader, 8, typeOfColumn.GetDecimal)
                    mDiscountPercent = GetValue(SqlReader, 9, typeOfColumn.GetDecimal)
                    mVATPercent = GetValue(SqlReader, 10, typeOfColumn.GetInt32)
                    mVATAmount = GetValue(SqlReader, 11, typeOfColumn.GetDecimal)
                    mAmountIncVAT = GetValue(SqlReader, 12, typeOfColumn.GetDecimal)
                    mDescriptions = GetValue(SqlReader, 13, typeOfColumn.GetString)
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
                arParams(0) = DBHelper.createParameter("@DocumentNo_", SqlDbType.NVarChar, ParameterDirection.Input, mDocumentNo_)
                arParams(1) = DBHelper.createParameter("@LineNo_", SqlDbType.Int, ParameterDirection.Input, mLineNo_)
                arParams(2) = DBHelper.createParameter("@LineType", SqlDbType.Int, ParameterDirection.Input, mLineType)
                arParams(3) = DBHelper.createParameter("@ItemNo_", SqlDbType.NVarChar, ParameterDirection.Input, mItemNo_)
                arParams(4) = DBHelper.createParameter("@ItemName", SqlDbType.NVarChar, ParameterDirection.Input, mItemName)
                arParams(5) = DBHelper.createParameter("@UnitofMeasure", SqlDbType.NVarChar, ParameterDirection.Input, mUnitofMeasure)
                arParams(6) = DBHelper.createParameter("@Quantity", SqlDbType.Decimal, ParameterDirection.Input, mQuantity)
                arParams(7) = DBHelper.createParameter("@UnitPriceIncVAT", SqlDbType.Decimal, ParameterDirection.Input, mUnitPriceIncVAT)
                arParams(8) = DBHelper.createParameter("@DiscountAmount", SqlDbType.Decimal, ParameterDirection.Input, mDiscountAmount)
                arParams(9) = DBHelper.createParameter("@DiscountPercent", SqlDbType.Decimal, ParameterDirection.Input, mDiscountPercent)
                arParams(10) = DBHelper.createParameter("@VATPercent", SqlDbType.Int, ParameterDirection.Input, mVATPercent)
                arParams(11) = DBHelper.createParameter("@VATAmount", SqlDbType.Decimal, ParameterDirection.Input, mVATAmount)
                arParams(12) = DBHelper.createParameter("@AmountIncVAT", SqlDbType.Decimal, ParameterDirection.Input, mAmountIncVAT)
                arParams(13) = DBHelper.createParameter("@Descriptions", SqlDbType.NVarChar, ParameterDirection.Input, mDescriptions)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_Sales Line", arParams)
                Return True
            Catch ex As Exception

                Return False
            End Try
        End Function
#End Region

    End Class

End Namespace