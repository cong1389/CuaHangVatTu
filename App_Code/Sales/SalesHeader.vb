Imports Microsoft.VisualBasic
Imports System.Data

Namespace adv.Business
    Public Class SalesHeader
#Region "Khai báo"
        Private mNo_ As String = ""
        Private mDocumentDate As String = ""
        Private mCartNo_ As String = ""
        Private mCustomerNo_ As String = ""
        Private mCustomerName As String = ""
        Private mShiptoName As String = ""
        Private mShiptoProvinceNo_ As String = ""
        Private mShiptoDistrictNo_ As String = ""
        Private mShiptoAddress As String = ""
        Private mShiptoTelephone As String = ""
        Private mShiptoCountry As String = ""
        Private mBilltoName As String = ""
        Private mBilltoAddress As String = ""
        Private mTaxCode As String = ""
        Private mVATInvoiceIssued As Integer = 0
        Private mDeliveryComments As String = ""
        Private mPaymentMethod As Integer = 0
        Private mPaymentStatus As Integer = 0
        Private mOrderStatus As Integer = 0
        Private mIPAddress As String = ""
        Private mDipositAmount As Double = 0
        Private mShiptoWardNo_ As String = ""
        Private mDeliveryDate As String = ""
        Private mDeliveryFrom As String = ""
        Private mDeliveryTo As String = ""
        Private mTimeDeliveryNo_ As String = ""
        Private mOrderTime As String = ""
        Private mShipToHouseNo_ As String = ""
        Private mDeliveryFee As Double = 0
        Property No_() As String
            Get
                Return mNo_
            End Get
            Set(ByVal value As String)
                mNo_ = value
            End Set
        End Property
        Property DocumentDate() As String
            Get
                Return mDocumentDate
            End Get
            Set(ByVal value As String)
                mDocumentDate = value
            End Set
        End Property
        Property CartNo_() As String
            Get
                Return mCartNo_
            End Get
            Set(ByVal value As String)
                mCartNo_ = value
            End Set
        End Property
        Property CustomerNo_() As String
            Get
                Return mCustomerNo_
            End Get
            Set(ByVal value As String)
                mCustomerNo_ = value
            End Set
        End Property
        Property CustomerName() As String
            Get
                Return mCustomerName
            End Get
            Set(ByVal value As String)
                mCustomerName = value
            End Set
        End Property
        Property ShiptoName() As String
            Get
                Return mShiptoName
            End Get
            Set(ByVal value As String)
                mShiptoName = value
            End Set
        End Property
        Property ShiptoProvinceNo_() As String
            Get
                Return mShiptoProvinceNo_
            End Get
            Set(ByVal value As String)
                mShiptoProvinceNo_ = value
            End Set
        End Property
        Property ShiptoDistrictNo_() As String
            Get
                Return mShiptoDistrictNo_
            End Get
            Set(ByVal value As String)
                mShiptoDistrictNo_ = value
            End Set
        End Property
        Property ShiptoAddress() As String
            Get
                Return mShiptoAddress
            End Get
            Set(ByVal value As String)
                mShiptoAddress = value
            End Set
        End Property
        Property ShiptoTelephone() As String
            Get
                Return mShiptoTelephone
            End Get
            Set(ByVal value As String)
                mShiptoTelephone = value
            End Set
        End Property
        Property ShiptoCountry() As String
            Get
                Return mShiptoCountry
            End Get
            Set(ByVal value As String)
                mShiptoCountry = value
            End Set
        End Property
        Property BilltoName() As String
            Get
                Return mBilltoName
            End Get
            Set(ByVal value As String)
                mBilltoName = value
            End Set
        End Property
        Property BilltoAddress() As String
            Get
                Return mBilltoAddress
            End Get
            Set(ByVal value As String)
                mBilltoAddress = value
            End Set
        End Property
        Property TaxCode() As String
            Get
                Return mTaxCode
            End Get
            Set(ByVal value As String)
                mTaxCode = value
            End Set
        End Property
        Property VATInvoiceIssued() As Integer
            Get
                Return mVATInvoiceIssued
            End Get
            Set(ByVal value As Integer)
                mVATInvoiceIssued = value
            End Set
        End Property
        Property DeliveryComments() As String
            Get
                Return mDeliveryComments
            End Get
            Set(ByVal value As String)
                mDeliveryComments = value
            End Set
        End Property
        Property PaymentMethod() As Integer
            Get
                Return mPaymentMethod
            End Get
            Set(ByVal value As Integer)
                mPaymentMethod = value
            End Set
        End Property
        Property PaymentStatus() As Integer
            Get
                Return mPaymentStatus
            End Get
            Set(ByVal value As Integer)
                mPaymentStatus = value
            End Set
        End Property
        Property OrderStatus() As Integer
            Get
                Return mOrderStatus
            End Get
            Set(ByVal value As Integer)
                mOrderStatus = value
            End Set
        End Property
        Property IPAddress() As String
            Get
                Return mIPAddress
            End Get
            Set(ByVal value As String)
                mIPAddress = value
            End Set
        End Property
        Property DipositAmount() As Double
            Get
                Return mDipositAmount
            End Get
            Set(ByVal value As Double)
                mDipositAmount = value
            End Set
        End Property
        Property ShiptoWardNo_() As String
            Get
                Return mShiptoWardNo_
            End Get
            Set(ByVal value As String)
                mShiptoWardNo_ = value
            End Set
        End Property
        Property DeliveryDate() As String
            Get
                Return mDeliveryDate
            End Get
            Set(ByVal value As String)
                mDeliveryDate = value
            End Set
        End Property
        Property DeliveryFrom() As String
            Get
                Return mDeliveryFrom
            End Get
            Set(ByVal value As String)
                mDeliveryFrom = value
            End Set
        End Property
        Property DeliveryTo() As String
            Get
                Return mDeliveryTo
            End Get
            Set(ByVal value As String)
                mDeliveryTo = value
            End Set
        End Property
        Property TimeDeliveryNo_() As String
            Get
                Return mTimeDeliveryNo_
            End Get
            Set(ByVal value As String)
                mTimeDeliveryNo_ = value
            End Set
        End Property
        Property ShipToHouseNo As String
            Get
                Return mShipToHouseNo_
            End Get
            Set(ByVal value As String)
                mShipToHouseNo_ = value
            End Set
        End Property
        Property DeliveryFee As Double
            Get
                Return mDeliveryFee
            End Get
            Set(ByVal value As Double)
                mDeliveryFee = value
            End Set
        End Property
        Property OrderTime As Date
            Get
                Return mOrderTime
            End Get
            Set(ByVal value As Date)
                mOrderTime = value
            End Set
        End Property

#End Region
#Region "Function"
        Public Function Load(ByVal sNo_ As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Sales Header] WHERE No_ = '" & sNo_ & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mDocumentDate = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mCartNo_ = GetValue(SqlReader, 2, typeOfColumn.GetString)
                    mCustomerNo_ = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mCustomerName = GetValue(SqlReader, 4, typeOfColumn.GetString)
                    mShiptoName = GetValue(SqlReader, 5, typeOfColumn.GetString)
                    mShiptoProvinceNo_ = GetValue(SqlReader, 6, typeOfColumn.GetString)
                    mShiptoDistrictNo_ = GetValue(SqlReader, 7, typeOfColumn.GetString)
                    mShiptoAddress = GetValue(SqlReader, 8, typeOfColumn.GetString)
                    mShiptoTelephone = GetValue(SqlReader, 9, typeOfColumn.GetString)
                    mShiptoCountry = GetValue(SqlReader, 10, typeOfColumn.GetString)
                    mBilltoName = GetValue(SqlReader, 11, typeOfColumn.GetString)
                    mBilltoAddress = GetValue(SqlReader, 12, typeOfColumn.GetString)
                    mTaxCode = GetValue(SqlReader, 13, typeOfColumn.GetString)
                    mVATInvoiceIssued = GetValue(SqlReader, 14, typeOfColumn.GetInt32)
                    mDeliveryComments = GetValue(SqlReader, 15, typeOfColumn.GetString)
                    mPaymentMethod = GetValue(SqlReader, 16, typeOfColumn.GetInt32)
                    mPaymentStatus = GetValue(SqlReader, 17, typeOfColumn.GetInt32)
                    mOrderStatus = GetValue(SqlReader, 18, typeOfColumn.GetInt32)
                    mIPAddress = GetValue(SqlReader, 19, typeOfColumn.GetString)
                    mDipositAmount = GetValue(SqlReader, 20, typeOfColumn.GetDecimal)
                    mShiptoWardNo_ = GetValue(SqlReader, 21, typeOfColumn.GetString)
                    mDeliveryDate = GetValue(SqlReader, 22, typeOfColumn.GetString)
                    mDeliveryFrom = GetValue(SqlReader, 23, typeOfColumn.GetString)
                    mDeliveryTo = GetValue(SqlReader, 24, typeOfColumn.GetString)
                    mTimeDeliveryNo_ = GetValue(SqlReader, 25, typeOfColumn.GetString)
                    mOrderTime = GetValue(SqlReader, 26, typeOfColumn.GetDateTime)
                    mShipToHouseNo_ = GetValue(SqlReader, 27, typeOfColumn.GetString)
                    mDeliveryFee = GetValue(SqlReader, 28, typeOfColumn.GetDecimal)
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


        Public Function LoadByCartNo(ByVal sCartNo_ As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Sales Header] WHERE [Cart No_] = '" & sCartNo_ & "' AND [Order Status] = 0 "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mDocumentDate = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mCartNo_ = GetValue(SqlReader, 2, typeOfColumn.GetString)
                    mCustomerNo_ = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mCustomerName = GetValue(SqlReader, 4, typeOfColumn.GetString)
                    mShiptoName = GetValue(SqlReader, 5, typeOfColumn.GetString)
                    mShiptoProvinceNo_ = GetValue(SqlReader, 6, typeOfColumn.GetString)
                    mShiptoDistrictNo_ = GetValue(SqlReader, 7, typeOfColumn.GetString)
                    mShiptoAddress = GetValue(SqlReader, 8, typeOfColumn.GetString)
                    mShiptoTelephone = GetValue(SqlReader, 9, typeOfColumn.GetString)
                    mShiptoCountry = GetValue(SqlReader, 10, typeOfColumn.GetString)
                    mBilltoName = GetValue(SqlReader, 11, typeOfColumn.GetString)
                    mBilltoAddress = GetValue(SqlReader, 12, typeOfColumn.GetString)
                    mTaxCode = GetValue(SqlReader, 13, typeOfColumn.GetString)
                    mVATInvoiceIssued = GetValue(SqlReader, 14, typeOfColumn.GetInt32)
                    mDeliveryComments = GetValue(SqlReader, 15, typeOfColumn.GetString)
                    mPaymentMethod = GetValue(SqlReader, 16, typeOfColumn.GetInt32)
                    mPaymentStatus = GetValue(SqlReader, 17, typeOfColumn.GetInt32)
                    mOrderStatus = GetValue(SqlReader, 18, typeOfColumn.GetInt32)
                    mIPAddress = GetValue(SqlReader, 19, typeOfColumn.GetString)
                    mDipositAmount = GetValue(SqlReader, 20, typeOfColumn.GetDecimal)
                    mShiptoWardNo_ = GetValue(SqlReader, 21, typeOfColumn.GetString)
                    mDeliveryDate = GetValue(SqlReader, 22, typeOfColumn.GetString)
                    mDeliveryFrom = GetValue(SqlReader, 23, typeOfColumn.GetString)
                    mDeliveryTo = GetValue(SqlReader, 24, typeOfColumn.GetString)
                    mTimeDeliveryNo_ = GetValue(SqlReader, 25, typeOfColumn.GetString)
                    mOrderTime = GetValue(SqlReader, 26, typeOfColumn.GetDateTime)
                    mShipToHouseNo_ = GetValue(SqlReader, 27, typeOfColumn.GetString)
                    mDeliveryFee = GetValue(SqlReader, 28, typeOfColumn.GetDecimal)
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
            Dim arParams() As IDataParameter = New IDataParameter(27) {}
            Try
                arParams(0) = DBHelper.createParameter("@No_", SqlDbType.NVarChar, ParameterDirection.Input, mNo_)
                arParams(1) = DBHelper.createParameter("@DocumentDate", SqlDbType.NVarChar, ParameterDirection.Input, mDocumentDate)
                arParams(2) = DBHelper.createParameter("@CartNo_", SqlDbType.NVarChar, ParameterDirection.Input, mCartNo_)
                arParams(3) = DBHelper.createParameter("@CustomerNo_", SqlDbType.NVarChar, ParameterDirection.Input, mCustomerNo_)
                arParams(4) = DBHelper.createParameter("@CustomerName", SqlDbType.NVarChar, ParameterDirection.Input, mCustomerName)
                arParams(5) = DBHelper.createParameter("@ShiptoName", SqlDbType.NVarChar, ParameterDirection.Input, mShiptoName)
                arParams(6) = DBHelper.createParameter("@ShiptoProvinceNo_", SqlDbType.NVarChar, ParameterDirection.Input, mShiptoProvinceNo_)
                arParams(7) = DBHelper.createParameter("@ShiptoDistrictNo_", SqlDbType.NVarChar, ParameterDirection.Input, mShiptoDistrictNo_)
                arParams(8) = DBHelper.createParameter("@ShiptoAddress", SqlDbType.NVarChar, ParameterDirection.Input, mShiptoAddress)
                arParams(9) = DBHelper.createParameter("@ShiptoTelephone", SqlDbType.NVarChar, ParameterDirection.Input, mShiptoTelephone)
                arParams(10) = DBHelper.createParameter("@ShiptoCountry", SqlDbType.NVarChar, ParameterDirection.Input, mShiptoCountry)
                arParams(11) = DBHelper.createParameter("@BilltoName", SqlDbType.NVarChar, ParameterDirection.Input, mBilltoName)
                arParams(12) = DBHelper.createParameter("@BilltoAddress", SqlDbType.NVarChar, ParameterDirection.Input, mBilltoAddress)
                arParams(13) = DBHelper.createParameter("@TaxCode", SqlDbType.NVarChar, ParameterDirection.Input, mTaxCode)
                arParams(14) = DBHelper.createParameter("@VATInvoiceIssued", SqlDbType.Int, ParameterDirection.Input, mVATInvoiceIssued)
                arParams(15) = DBHelper.createParameter("@DeliveryComments", SqlDbType.NVarChar, ParameterDirection.Input, mDeliveryComments)
                arParams(16) = DBHelper.createParameter("@PaymentMethod", SqlDbType.Int, ParameterDirection.Input, mPaymentMethod)
                arParams(17) = DBHelper.createParameter("@PaymentStatus", SqlDbType.Int, ParameterDirection.Input, mPaymentStatus)
                arParams(18) = DBHelper.createParameter("@OrderStatus", SqlDbType.Int, ParameterDirection.Input, mOrderStatus)
                arParams(19) = DBHelper.createParameter("@IPAddress", SqlDbType.NVarChar, ParameterDirection.Input, mIPAddress)
                arParams(20) = DBHelper.createParameter("@DipositAmount", SqlDbType.Decimal, ParameterDirection.Input, mDipositAmount)
                arParams(21) = DBHelper.createParameter("@ShiptoWardNo_", SqlDbType.NVarChar, ParameterDirection.Input, mShiptoWardNo_)
                arParams(22) = DBHelper.createParameter("@DeliveryDate", SqlDbType.NVarChar, ParameterDirection.Input, mDeliveryDate)
                arParams(23) = DBHelper.createParameter("@DeliveryFrom", SqlDbType.NVarChar, ParameterDirection.Input, mDeliveryFrom)
                arParams(24) = DBHelper.createParameter("@DeliveryTo", SqlDbType.NVarChar, ParameterDirection.Input, mDeliveryTo)
                arParams(25) = DBHelper.createParameter("@TimeDeliveryNo_", SqlDbType.NVarChar, ParameterDirection.Input, mTimeDeliveryNo_)
                arParams(26) = DBHelper.createParameter("@ShipToHouseNo_", SqlDbType.NVarChar, ParameterDirection.Input, mShipToHouseNo_)
                arParams(27) = DBHelper.createParameter("@DeliveryFee", SqlDbType.Decimal, ParameterDirection.Input, mDeliveryFee)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_SalesHeader", arParams)
                Return True
            Catch ex As Exception

                Return False
            End Try
        End Function

        Function GetAmountPayment(ByVal sCartNo As String) As Double
            Dim SQL As String
            Sql = " SELECT SUM(A.AmountRemaining) FROM " & _
             " (SELECT SUM([Amount Inc VAT]) - SUM([Discount Amount]) AmountRemaining FROM [Cart Line] WHERE [Cart No_] = '" & sCartNo & "' " & _
             " UNION ALL " & _
             " SELECT - ISNULL(SUM(Amount),0) AmountRemaining FROM [Payment Entry] WHERE [Payment Type] = 0 AND [Cart No_] = '" & sCartNo & "') A "
            Dim nRemainingAmount As Double = 0
            nRemainingAmount = ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, Sql), 0)
            Return nRemainingAmount
        End Function

        Sub GetTimeDelivery(ByRef sFrom As String, ByRef sTo As String, ByVal sTimeNo As String)
            Dim SQL As String
            SQL = " SELECT * FROM [Time Delivery] WHERE No_ = '" & sTimeNo & "'"
            Dim SqlReader As IDataReader
            SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, SQL)
            sFrom = ""
            sTo = ""
            If SqlReader.Read() Then
                sFrom = SqlReader.GetString(2)
                sTo = SqlReader.GetString(3)
            End If
        End Sub

        Function FinishedConfirm(ByVal sOrderNo As String) As Boolean

            Dim arParams() As IDataParameter = New IDataParameter(2) {}
            Try
                arParams(0) = DBHelper.createParameter("@No_", SqlDbType.NVarChar, ParameterDirection.Input, sOrderNo)
                arParams(1) = DBHelper.createParameter("@ItemFeeNo", SqlDbType.NVarChar, ParameterDirection.Input, GetPakingFeeItemNo())
                arParams(2) = DBHelper.createParameter("@BigItemFeeNo", SqlDbType.NVarChar, ParameterDirection.Input, GetBigItemFeeItemNo())
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_ConfirmSalesOrder", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Sub UpdatePaymentMethod(ByVal sOrderNo As String, ByVal iPaymentMethod As Integer)
            Dim SQL As String
            SQL = " UPDATE [Sales Header] SET [Payment Method] = " & iPaymentMethod & " WHERE No_ = '" & sOrderNo & "'"
            DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, Sql)
        End Sub

        Sub NotFinished(ByVal sOrderNo As String)
            Dim SQL As String
            SQL = " DELETE FROM [Sales Header] WHERE No_ = '" & sOrderNo & "'"
            DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
            SQL = " DELETE FROM [Sales Line] WHERE [Document No_] = '" & sOrderNo & "'"
            DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
        End Sub

        Sub UpdateStatus(ByVal sOrderNo As String, ByVal iStatus As Integer)
            Dim SQL As String
            SQL = " UPDATE [Sales Header] SET [Order Status] = " & iStatus & " WHERE No_ = '" & sOrderNo & "'"
            DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
        End Sub

        Sub ApprovalByHomeone(ByVal sOrderNo As String)
            Load(sOrderNo)
            Dim objC As New adv.Business.Customer
            UpdateStatus(sOrderNo, 2)
            Dim objWM As New adv.Business.WebEmail
            objWM.Subject = "Đóng gói đơn hàng"
            objWM.FromAddress = GetEmailAdd()
            objWM.ToAddress = GetEmailPacking()
            objWM.IsEmailBodyHtml = True
            Dim sEmailTmp As String = ""
            sEmailTmp &= "Chuẩn bị đơn hàng:"
            sEmailTmp &= ShowOrderForReparing(sOrderNo, False)
            objWM.EmailBody = sEmailTmp
            objWM.SendMail()
            objC.Load(mCustomerNo_)
            sEmailTmp = ShowOrder(sOrderNo, True)
            SendEmailToCustomer(sEmailTmp, "Chấp nhận đơn hàng", objC.Email, "W005")
        End Sub

        Sub SendMailPacking(ByVal sOrdrNo As String)
            Dim objSH As New adv.Business.SalesHeader
            Dim objWM As New adv.Business.WebEmail
            objWM.Subject = "Đóng gói đơn hàng"
            objWM.FromAddress = GetEmailAdd()

            objWM.ToAddress = GetEmailPacking()
            objWM.IsEmailBodyHtml = True
            Dim sEmailTmp As String = ""
            sEmailTmp &= "Chuẩn bị đơn hàng:"
            sEmailTmp &= ShowOrderForReparing(sOrdrNo, False)
            objWM.EmailBody = sEmailTmp
            objWM.SendMail()
        End Sub

        Function IsFreshOrder(ByVal sOrderNo As String) As Boolean
            Dim SQL As String
            SQL = " SELECT TOP 1 ISNULL(SL.[Item No_],'') [Item No_] FROM [Sales Line] SL INNER JOIN Item I ON SL.[Item No_] = I.No_ WHERE SL.[Document No_] = '" & sOrderNo & "' AND I.[Menu Division No_] = '28' "
            Dim sItemNo As String = ""
            sItemNo = ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), "").ToString.Trim
            Return sItemNo.Trim <> ""
        End Function

        Function GetAmountInCart(ByVal sCartNo As String) As Double
            Dim SQL = "SELECT SUM([Amount Inc VAT]) FROM [Cart Line] WHERE [Cart No_] = '" & sCartNo & "'"
            Return ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), 0)
        End Function

        Sub Delivery(ByVal sOrderNo As String, Optional ByVal SendEmail As Boolean = False)
            UpdateStatus(sOrderNo, 3)
            If SendEmail Then
                Dim objWM As New adv.Business.WebEmail
                Dim objC As New adv.Business.Customer
                Load(sOrderNo)
                objC.Load(mCustomerNo_)
                objWM.ToAddress = objC.Email
                objWM.Subject = "Giao hàng"
                objWM.FromAddress = GetEmailAdd()
                objWM.IsEmailBodyHtml = True
                Dim sEmailTmp As String = ""
                Dim objMT As New adv.Business.MsgContent
                objMT.Load("W007")
                sEmailTmp &= objMT.Content
                sEmailTmp = sEmailTmp.Replace("[ORDERNO]", sOrderNo)
                objWM.EmailBody = sEmailTmp
                objWM.SendMail()
            End If
        End Sub

        Sub Finished(ByVal sOrderNo As String)
            UpdateStatus(sOrderNo, 4)
            CalculateFinished(sOrderNo)
        End Sub

        Sub Cancel(ByVal sOrderNo As String, Optional ByVal SendEmail As Boolean = False, Optional ByVal sReason As String = "")
            Dim SQL As String
            SQL = "EXEC CancelOrder '" & sOrderNo & "'"
            DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
            If SendEmail Then
                Dim objWM As New adv.Business.WebEmail
                Dim objC As New adv.Business.Customer
                Load(sOrderNo)
                objC.Load(mCustomerNo_)
                objWM.ToAddress = objC.Email
                objWM.Subject = "Hủy đơn hàng"
                objWM.FromAddress = GetEmailAdd()
                objWM.IsEmailBodyHtml = True
                Dim sEmailTmp As String = ""
                Dim objMT As New adv.Business.MsgContent
                objMT.Load("W006")
                sEmailTmp &= objMT.Content
                sEmailTmp = sEmailTmp.Replace("[ORDERNO]", sOrderNo)
                sEmailTmp = sEmailTmp.Replace("[REASON]", sReason)
                objWM.EmailBody = sEmailTmp
                objWM.SendMail()
            End If
        End Sub

        Function CalculateFinished(ByVal sOrderNo As String) As Boolean
            Dim arParams() As IDataParameter = New IDataParameter(1) {}
            Try
                arParams(0) = DBHelper.createParameter("@OrderNo", SqlDbType.NVarChar, ParameterDirection.Input, sOrderNo)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "W_FinishedOrder", arParams)
                Dim SQL As String
                Dim objC As New adv.Business.Customer
                SQL = "SELECT [Customer No_], Amount, Descriptions FROM [Point Ledger Entry] WHERE [Document No_] = '" & sOrderNo & "' ORDER BY [Entry No_]"
                Dim tbl As DataTable
                tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
                Dim nIJ As Integer
                Dim sMailContent As String = ""
                Dim nBlance As Double
                For nIJ = 0 To tbl.Rows.Count - 1
                    nBlance = objC.GetBalanceAmount(tbl.Rows(nIJ).Item("Customer No_"))
                    objC.Load(tbl.Rows(nIJ).Item("Customer No_"))
                    sMailContent = "Dear " & objC.FullName & "<br /><br />"
                    sMailContent &= "Số dư tài khoản của bạn là: " & Format(Math.Round(nBlance, 0), "##,###,###").Replace(",", ".") & " đồng. <br /> <br /> Giao dịch gần nhất: "
                    sMailContent &= Format(Math.Round(tbl.Rows(nIJ).Item("Amount"), 0), "##,###,###").Replace(",", ".") & "<br /> <br />"
                    sMailContent &= "Lý do: " & tbl.Rows(nIJ).Item("Descriptions") & "<br /> <br /> <br />"
                    sMailContent &= "Bạn có thể đăng nhập vào tài khoản và kiểm tra chi tiết phát sinh.<br /><br /><br />"
                    sMailContent &= "Trân trọng!"
                    SendEmailToCustomer(sMailContent, "Số dư tài khoản thưởng", objC.Email)
                Next

                Return True
            Catch
                Return False
            End Try
        End Function

        Function UpdateDeliveryFeeByVolume(ByVal sCartNo As String, ByVal sProvinceNo As String, ByVal sDistrictNo As String) As Boolean
            Dim arParams() As IDataParameter = New IDataParameter(2) {}
            Try
                arParams(0) = DBHelper.createParameter("@CartNo_", SqlDbType.NVarChar, ParameterDirection.Input, sCartNo)
                arParams(1) = DBHelper.createParameter("@ProvinceNo_", SqlDbType.NVarChar, ParameterDirection.Input, sProvinceNo)
                arParams(2) = DBHelper.createParameter("@DistrictNo_", SqlDbType.NVarChar, ParameterDirection.Input, sDistrictNo)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "CalculateDeliveryFee", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

#End Region

    End Class
End Namespace