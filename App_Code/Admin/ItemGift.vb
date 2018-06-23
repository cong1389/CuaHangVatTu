Imports System.Data
Imports Microsoft.VisualBasic

Namespace adv.Business
    Public Class ItemGift
#Region "Khai báo"
        Private mItemNo_ As String = ""
        Private mLineNo_ As Integer = 0
        Private mUnitofMeasureNo_ As String = ""
        Private mQuantity As Double = 0
        Private mDiscountNo_ As String = ""
        Private mGiftItemNo_ As String = ""
        Private mGiftItemName As String = ""
        Private mGiftUnitofMeasureNo_ As String = ""
        Private mGiftQuantity As Double = 0
        Private mStartingDate As String = ""
        Private mEndingDate As String = ""
        Private mAreaCode As String = ""
        Private mSalesCode As String = ""

        Property ItemNo_() As String
            Get
                Return mItemNo_
            End Get
            Set(ByVal value As String)
                mItemNo_ = value
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
        Property UnitofMeasureNo_() As String
            Get
                Return mUnitofMeasureNo_
            End Get
            Set(ByVal value As String)
                mUnitofMeasureNo_ = value
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
        Property DiscountNo_() As String
            Get
                Return mDiscountNo_
            End Get
            Set(ByVal value As String)
                mDiscountNo_ = value
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
        Property GiftItemName() As String
            Get
                Return mGiftItemName
            End Get
            Set(ByVal value As String)
                mGiftItemName = value
            End Set
        End Property
        Property GiftUnitofMeasureNo_() As String
            Get
                Return mGiftUnitofMeasureNo_
            End Get
            Set(ByVal value As String)
                mGiftUnitofMeasureNo_ = value
            End Set
        End Property
        Property GiftQuantity() As Double
            Get
                Return mGiftQuantity
            End Get
            Set(ByVal value As Double)
                mGiftQuantity = value
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
        Property AreaCode() As String
            Get
                Return mAreaCode
            End Get
            Set(ByVal value As String)
                mAreaCode = value
            End Set
        End Property
        Property SalesCode() As String
            Get
                Return mSalesCode
            End Get
            Set(ByVal value As String)
                mSalesCode = value
            End Set
        End Property

#End Region
#Region "Function"
        Public Function Load(ByVal sItemNo As String, ByVal nLineNo As Integer) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Item Gift] WHERE [Item No_] = '" & sItemNo & "' AND [Line No_] = " & nLineNo
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mItemNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mLineNo_ = GetValue(SqlReader, 1, typeOfColumn.GetInt32)
                    mUnitofMeasureNo_ = GetValue(SqlReader, 2, typeOfColumn.GetString)
                    mQuantity = GetValue(SqlReader, 3, typeOfColumn.GetDecimal)
                    mDiscountNo_ = GetValue(SqlReader, 4, typeOfColumn.GetString)
                    mGiftItemNo_ = GetValue(SqlReader, 5, typeOfColumn.GetString)
                    mGiftItemName = GetValue(SqlReader, 6, typeOfColumn.GetString)
                    mGiftUnitofMeasureNo_ = GetValue(SqlReader, 7, typeOfColumn.GetString)
                    mGiftQuantity = GetValue(SqlReader, 8, typeOfColumn.GetDecimal)
                    mStartingDate = GetValue(SqlReader, 9, typeOfColumn.GetString)
                    mEndingDate = GetValue(SqlReader, 10, typeOfColumn.GetString)
                    mAreaCode = GetValue(SqlReader, 11, typeOfColumn.GetString)
                    mSalesCode = GetValue(SqlReader, 12, typeOfColumn.GetString)
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
            Dim arParams() As IDataParameter = New IDataParameter(12) {}
            Try
                arParams(0) = DBHelper.createParameter("@ItemNo_", SqlDbType.NVarChar, ParameterDirection.Input, mItemNo_)
                arParams(1) = DBHelper.createParameter("@LineNo_", SqlDbType.Int, ParameterDirection.Input, mLineNo_)
                arParams(2) = DBHelper.createParameter("@UnitofMeasureNo_", SqlDbType.NVarChar, ParameterDirection.Input, mUnitofMeasureNo_)
                arParams(3) = DBHelper.createParameter("@Quantity", SqlDbType.Decimal, ParameterDirection.Input, mQuantity)
                arParams(4) = DBHelper.createParameter("@DiscountNo_", SqlDbType.NVarChar, ParameterDirection.Input, mDiscountNo_)
                arParams(5) = DBHelper.createParameter("@GiftItemNo_", SqlDbType.NVarChar, ParameterDirection.Input, mGiftItemNo_)
                arParams(6) = DBHelper.createParameter("@GiftItemName", SqlDbType.NVarChar, ParameterDirection.Input, mGiftItemName)
                arParams(7) = DBHelper.createParameter("@GiftUnitofMeasureNo_", SqlDbType.NVarChar, ParameterDirection.Input, mGiftUnitofMeasureNo_)
                arParams(8) = DBHelper.createParameter("@GiftQuantity", SqlDbType.Decimal, ParameterDirection.Input, mGiftQuantity)
                arParams(9) = DBHelper.createParameter("@StartingDate", SqlDbType.NVarChar, ParameterDirection.Input, mStartingDate)
                arParams(10) = DBHelper.createParameter("@EndingDate", SqlDbType.NVarChar, ParameterDirection.Input, mEndingDate)
                arParams(11) = DBHelper.createParameter("@AreaCode", SqlDbType.NVarChar, ParameterDirection.Input, mAreaCode)
                arParams(12) = DBHelper.createParameter("@SalesCode", SqlDbType.NVarChar, ParameterDirection.Input, mSalesCode)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_ItemGift", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
#End Region

    End Class
End Namespace