Imports Microsoft.VisualBasic
Imports System.Data

Namespace adv.Business
    Public Class Customer
#Region "Khai báo"
        Private mNo_ As String = ""
        Private mFullName As String = ""
        Private mSex As Integer = 0
        Private mTelephone As String = ""
        Private mEmail As String = ""
        Private mAddress As String = ""
        Private mProvinceNo_ As String = ""
        Private mDistrictNo_ As String = ""
        Private mBirthday As String = ""
        Private mFullAddress As String = ""
        Private mBillToName As String = ""
        Private mBillToAddress As String = ""
        Private mTaxCode As String = ""
        Private mCustomerType As String = ""
        Private mCustomerPriceGroup As String = ""
        Private mLoyaltyCardNo_ As String = ""
        Private mRegisterDate As String = ""
        Private mLastVisited As String = ""
        Private mPassword As String = ""
        Private mWardNo_ As String
        Private mGuestID As String = ""
        Private mReferenceCode As String = ""
        Private mIdentificationCode As String = ""
        Private mReceivingEmail As Integer = 1
        Private mKeyValue As String = ""
        Property No_() As String
            Get
                Return mNo_
            End Get
            Set(ByVal value As String)
                mNo_ = value
            End Set
        End Property
        Property FullName() As String
            Get
                Return mFullName
            End Get
            Set(ByVal value As String)
                mFullName = value
            End Set
        End Property
        Property Sex() As Integer
            Get
                Return mSex
            End Get
            Set(ByVal value As Integer)
                mSex = value
            End Set
        End Property
        Property Telephone() As String
            Get
                Return mTelephone
            End Get
            Set(ByVal value As String)
                mTelephone = value
            End Set
        End Property
        Property Email() As String
            Get
                Return mEmail
            End Get
            Set(ByVal value As String)
                mEmail = value
            End Set
        End Property
        Property Address() As String
            Get
                Return mAddress
            End Get
            Set(ByVal value As String)
                mAddress = value
            End Set
        End Property
        Property ProvinceNo_() As String
            Get
                Return mProvinceNo_
            End Get
            Set(ByVal value As String)
                mProvinceNo_ = value
            End Set
        End Property
        Property DistrictNo_() As String
            Get
                Return mDistrictNo_
            End Get
            Set(ByVal value As String)
                mDistrictNo_ = value
            End Set
        End Property
        Property Birthday() As String
            Get
                Return mBirthday
            End Get
            Set(ByVal value As String)
                mBirthday = value
            End Set
        End Property
        Property FullAddress() As String
            Get
                Return mFullAddress
            End Get
            Set(ByVal value As String)
                mFullAddress = value
            End Set
        End Property
        Property BillToName() As String
            Get
                Return mBillToName
            End Get
            Set(ByVal value As String)
                mBillToName = value
            End Set
        End Property
        Property BillToAddress() As String
            Get
                Return mBillToAddress
            End Get
            Set(ByVal value As String)
                mBillToAddress = value
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
        Property CustomerType() As String
            Get
                Return mCustomerType
            End Get
            Set(ByVal value As String)
                mCustomerType = value
            End Set
        End Property
        Property CustomerPriceGroup() As String
            Get
                Return mCustomerPriceGroup
            End Get
            Set(ByVal value As String)
                mCustomerPriceGroup = value
            End Set
        End Property
        Property LoyaltyCardNo_() As String
            Get
                Return mLoyaltyCardNo_
            End Get
            Set(ByVal value As String)
                mLoyaltyCardNo_ = value
            End Set
        End Property
        Property RegisterDate() As String
            Get
                Return mRegisterDate
            End Get
            Set(ByVal value As String)
                mRegisterDate = value
            End Set
        End Property
        Property LastVisited() As String
            Get
                Return mLastVisited
            End Get
            Set(ByVal value As String)
                mLastVisited = value
            End Set
        End Property
        Property Password() As String
            Get
                Return mPassword
            End Get
            Set(ByVal value As String)
                mPassword = value
            End Set
        End Property
        Property WardNo_ As String
            Get
                Return mWardNo_
            End Get
            Set(ByVal value As String)
                mWardNo_ = value
            End Set
        End Property
        Property GuestID() As String
            Get
                Return mGuestID
            End Get
            Set(ByVal value As String)
                mGuestID = value
            End Set
        End Property
        Property ReferenceCode() As String
            Get
                Return mReferenceCode
            End Get
            Set(ByVal value As String)
                mReferenceCode = value
            End Set
        End Property
        Property IdentificationCode() As String
            Get
                Return mIdentificationCode
            End Get
            Set(ByVal value As String)
                mIdentificationCode = value
            End Set
        End Property
        Property ReceivingEmail As Integer
            Get
                Return mReceivingEmail
            End Get
            Set(ByVal value As Integer)
                mReceivingEmail = value
            End Set
        End Property
        Property KeyValue As String
            Get
                Return mKeyValue
            End Get
            Set(ByVal value As String)
                mKeyValue = value
            End Set
        End Property

#End Region
#Region "Function"
        Public Function Load(ByVal sNo As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Customer] WHERE No_ = '" & sNo & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mFullName = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mSex = GetValue(SqlReader, 2, typeOfColumn.GetInt32)
                    mTelephone = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mEmail = GetValue(SqlReader, 4, typeOfColumn.GetString)
                    mAddress = GetValue(SqlReader, 5, typeOfColumn.GetString)
                    mProvinceNo_ = GetValue(SqlReader, 6, typeOfColumn.GetString)
                    mDistrictNo_ = GetValue(SqlReader, 7, typeOfColumn.GetString)
                    mBirthday = GetValue(SqlReader, 8, typeOfColumn.GetString)
                    mFullAddress = GetValue(SqlReader, 9, typeOfColumn.GetString)
                    mBillToName = GetValue(SqlReader, 10, typeOfColumn.GetString)
                    mBillToAddress = GetValue(SqlReader, 11, typeOfColumn.GetString)
                    mTaxCode = GetValue(SqlReader, 12, typeOfColumn.GetString)
                    mCustomerType = GetValue(SqlReader, 13, typeOfColumn.GetString)
                    mCustomerPriceGroup = GetValue(SqlReader, 14, typeOfColumn.GetString)
                    mLoyaltyCardNo_ = GetValue(SqlReader, 15, typeOfColumn.GetString)
                    mRegisterDate = GetValue(SqlReader, 16, typeOfColumn.GetString)
                    mLastVisited = GetValue(SqlReader, 17, typeOfColumn.GetString)
                    mPassword = GetValue(SqlReader, 18, typeOfColumn.GetString)
                    mWardNo_ = GetValue(SqlReader, 19, typeOfColumn.GetString)
                    mGuestID = GetValue(SqlReader, 20, typeOfColumn.GetString)
                    mReferenceCode = GetValue(SqlReader, 21, typeOfColumn.GetString)
                    mIdentificationCode = GetValue(SqlReader, 22, typeOfColumn.GetString)
                    mReceivingEmail = GetValue(SqlReader, 23, typeOfColumn.GetInt32)
                    mKeyValue = GetValue(SqlReader, 24, typeOfColumn.GetString)
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

        Public Function LoadByEmail(ByVal sEmail As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Customer] WHERE Email = '" & sEmail & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mFullName = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mSex = GetValue(SqlReader, 2, typeOfColumn.GetInt32)
                    mTelephone = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mEmail = GetValue(SqlReader, 4, typeOfColumn.GetString)
                    mAddress = GetValue(SqlReader, 5, typeOfColumn.GetString)
                    mProvinceNo_ = GetValue(SqlReader, 6, typeOfColumn.GetString)
                    mDistrictNo_ = GetValue(SqlReader, 7, typeOfColumn.GetString)
                    mBirthday = GetValue(SqlReader, 8, typeOfColumn.GetString)
                    mFullAddress = GetValue(SqlReader, 9, typeOfColumn.GetString)
                    mBillToName = GetValue(SqlReader, 10, typeOfColumn.GetString)
                    mBillToAddress = GetValue(SqlReader, 11, typeOfColumn.GetString)
                    mTaxCode = GetValue(SqlReader, 12, typeOfColumn.GetString)
                    mCustomerType = GetValue(SqlReader, 13, typeOfColumn.GetString)
                    mCustomerPriceGroup = GetValue(SqlReader, 14, typeOfColumn.GetString)
                    mLoyaltyCardNo_ = GetValue(SqlReader, 15, typeOfColumn.GetString)
                    mRegisterDate = GetValue(SqlReader, 16, typeOfColumn.GetString)
                    mLastVisited = GetValue(SqlReader, 17, typeOfColumn.GetString)
                    mPassword = GetValue(SqlReader, 18, typeOfColumn.GetString)
                    mWardNo_ = GetValue(SqlReader, 19, typeOfColumn.GetString)
                    mGuestID = GetValue(SqlReader, 20, typeOfColumn.GetString)
                    mReferenceCode = GetValue(SqlReader, 21, typeOfColumn.GetString)
                    mIdentificationCode = GetValue(SqlReader, 22, typeOfColumn.GetString)
                    mReceivingEmail = GetValue(SqlReader, 23, typeOfColumn.GetInt32)
                    mKeyValue = GetValue(SqlReader, 24, typeOfColumn.GetString)
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
            Dim arParams() As IDataParameter = New IDataParameter(24) {}
            Try
                mKeyValue = adv.Business.MD5Encrypt("KEY" & mEmail)
                arParams(0) = DBHelper.createParameter("@No_", SqlDbType.NVarChar, ParameterDirection.Input, mNo_)
                arParams(1) = DBHelper.createParameter("@FullName", SqlDbType.NVarChar, ParameterDirection.Input, mFullName)
                arParams(2) = DBHelper.createParameter("@Sex", SqlDbType.Int, ParameterDirection.Input, mSex)
                arParams(3) = DBHelper.createParameter("@Telephone", SqlDbType.NVarChar, ParameterDirection.Input, mTelephone)
                arParams(4) = DBHelper.createParameter("@Email", SqlDbType.NVarChar, ParameterDirection.Input, mEmail)
                arParams(5) = DBHelper.createParameter("@Address", SqlDbType.NVarChar, ParameterDirection.Input, mAddress)
                arParams(6) = DBHelper.createParameter("@ProvinceNo_", SqlDbType.NVarChar, ParameterDirection.Input, mProvinceNo_)
                arParams(7) = DBHelper.createParameter("@DistrictNo_", SqlDbType.NVarChar, ParameterDirection.Input, mDistrictNo_)
                arParams(8) = DBHelper.createParameter("@Birthday", SqlDbType.NVarChar, ParameterDirection.Input, mBirthday)
                arParams(9) = DBHelper.createParameter("@FullAddress", SqlDbType.NVarChar, ParameterDirection.Input, mFullAddress)
                arParams(10) = DBHelper.createParameter("@BillToName", SqlDbType.NVarChar, ParameterDirection.Input, mBillToName)
                arParams(11) = DBHelper.createParameter("@BillToAddress", SqlDbType.NVarChar, ParameterDirection.Input, mBillToAddress)
                arParams(12) = DBHelper.createParameter("@TaxCode", SqlDbType.NVarChar, ParameterDirection.Input, mTaxCode)
                arParams(13) = DBHelper.createParameter("@CustomerType", SqlDbType.NVarChar, ParameterDirection.Input, mCustomerType)
                arParams(14) = DBHelper.createParameter("@CustomerPriceGroup", SqlDbType.NVarChar, ParameterDirection.Input, mCustomerPriceGroup)
                arParams(15) = DBHelper.createParameter("@LoyaltyCardNo_", SqlDbType.NVarChar, ParameterDirection.Input, mLoyaltyCardNo_)
                arParams(16) = DBHelper.createParameter("@RegisterDate", SqlDbType.NVarChar, ParameterDirection.Input, mRegisterDate)
                arParams(17) = DBHelper.createParameter("@LastVisited", SqlDbType.NVarChar, ParameterDirection.Input, mLastVisited)
                arParams(18) = DBHelper.createParameter("@Password", SqlDbType.NVarChar, ParameterDirection.Input, mPassword)
                arParams(19) = DBHelper.createParameter("@WardNo_", SqlDbType.NVarChar, ParameterDirection.Input, mWardNo_)
                arParams(20) = DBHelper.createParameter("@GuestID", SqlDbType.NVarChar, ParameterDirection.Input, mGuestID)
                arParams(21) = DBHelper.createParameter("@ReferenceCode", SqlDbType.NVarChar, ParameterDirection.Input, mReferenceCode)
                arParams(22) = DBHelper.createParameter("@IdentificationCode", SqlDbType.NVarChar, ParameterDirection.Input, mIdentificationCode)
                arParams(23) = DBHelper.createParameter("@ReceivingEmail", SqlDbType.Int, ParameterDirection.Input, mReceivingEmail)
                arParams(24) = DBHelper.createParameter("@KeyValue", SqlDbType.NVarChar, ParameterDirection.Input, mKeyValue)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_Customer", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Function GetZone(ByVal sWardNo As String, ByVal sDistrictNo As String) As String
            Dim SQL As String
            SQL = "SELECT [Zone No_] FROM [Ward District] WHERE [Ward No_] = '" & sWardNo & "' AND [District No_] = '" & sDistrictNo & "'"
            Return ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), "").ToString.Trim
        End Function

        Function GetAddress(ByVal sHouseNo As String, ByVal sWardNo As String, ByVal sDistrictNo As String, ByVal sProvinceNo As String) As String
            Dim SQL As String
            Dim sAddress As String = sHouseNo
            SQL = " SELECT Name FROM Province WHERE  No_ = '" & sDistrictNo & "'"
            sAddress &= " - " & ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), "").ToString.Trim
            SQL = " SELECT Name FROM Province WHERE  No_ = '" & sProvinceNo & "'"
            sAddress &= " - " & ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), "").ToString.Trim
            Return sAddress
        End Function

        Function ResetPassword(ByVal sEmail As String) As String
            Dim sPwd As String = ""
            Dim nIJ As Integer
            Dim Rd As New System.Random
            Try
                For nIJ = 1 To 6
                    sPwd &= Rd.Next(0, 9)
                Next
                Dim sMD5pwd As String
                sMD5pwd = adv.Business.MD5Encrypt(sEmail.Trim & sPwd)
                Dim SQL As String
                SQL = " UPDATE Customer SET Password = '" & sMD5pwd & "' WHERE Email = '" & sEmail & "'"
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
                Return sPwd
            Catch ex As Exception
                Return ""
            End Try
        End Function

        Function ExistsRefrenceCode(ByVal sCode As String) As Boolean
            Dim SQL As String
            SQL = "SELECT No_ FROM Customer WHERE [Identification Code] = '" & sCode & "'"
            Dim sTmp As String
            sTmp = ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), "").ToString.Trim
            Return sTmp.Trim <> ""
        End Function

        Function ChangePwd(ByVal sCustomerNo As String, ByVal sNewPwd As String) As Boolean
            Try
                Dim SQL As String
                SQL = " UPDATE Customer SET [Password] = '" & sNewPwd & "' WHERE No_ = '" & sCustomerNo & "'"
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Function GetBalanceAmount(ByVal sCustomerNo As String) As Double
            Dim SQL As String
            SQL = " SELECT SUM(Amount) [Balance Amount] FROM [Point Ledger Entry] WHERE [Customer No_]  = '" & sCustomerNo & "'"
            Return ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), 0)
        End Function

        Function GetNumOfRef(ByVal sRefCode As String) As Double
            Dim SQL As String

            SQL = " SELECT COUNT(No_) FROM Customer WHERE [Reference Code] = '" & sRefCode & "' "
            Return ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), 0)

        End Function

        Function GetNumOfBuy(ByVal sRefCode As String) As Double
            Dim SQL As String
            SQL = " SELECT COUNT(DISTINCT([Customer No_])) FROM [Sales Header] WHERE [Order Status] = 4 AND [Customer No_] IN (SELECT No_ FROM Customer WHERE [Reference Code] = '" & sRefCode & "' ) "
            Return ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), 0)
        End Function

        Function UpdateCouponNo(ByVal sCustomerNo As String, ByVal sCouponNo As String) As Boolean
            Dim SQL As String
            SQL = " UPDATE [Card Data Entry] SET [Customer No_] = '" & sCustomerNo & "' WHERE [Entry Code] = '" & sCouponNo & "'"
            Try
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Function GetCouponNo(ByVal sCustomerNo As String) As String
            Dim SQL As String
            SQL = " SELECT TOP 1 [Entry Code] FROM [Card Data Entry] WHERE [Customer No_] = '" & sCustomerNo & "' ORDER BY [Ending Date] DESC "
            Return ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), "").ToString.Trim
        End Function

        Function ExistsID(ByVal sNewID As String, ByVal sCustomerNo As String) As Boolean
            Dim SQL As String
            SQL = " SELECT No_ FROM Customer WHERE No_  <> '" & sCustomerNo & "' AND [Identification Code] = '" & sNewID & "'"
            Dim sTmp As String
            sTmp = DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL)
            Return ReturnIfNull(sTmp, "").ToString.Trim <> ""
        End Function
#End Region

    End Class
End Namespace