Imports Microsoft.VisualBasic
Imports System.Data

Namespace adv.Business

    Public Class CustomerReview
#Region "Khai báo"
        Private mLineNo_ As Integer = 0
        Private mItemNo_ As String = ""
        Private mCustomerNo_ As String = ""
        Private mCustomerName As String = ""
        Private mEmail As String = ""
        Private mReviewTitle As String = ""
        Private mReviewText As String = ""
        Private mReviewDate As String = ""
        Private mCustomerIP As String = ""
        Private mCustomerRate As Double = 0
        Private mPublished As Integer = 0
        Private mReviewHour As String = ""
        Private mGuestID As String = ""

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
        Property Email() As String
            Get
                Return mEmail
            End Get
            Set(ByVal value As String)
                mEmail = value
            End Set
        End Property
        Property ReviewTitle() As String
            Get
                Return mReviewTitle
            End Get
            Set(ByVal value As String)
                mReviewTitle = value
            End Set
        End Property
        Property ReviewText() As String
            Get
                Return mReviewText
            End Get
            Set(ByVal value As String)
                mReviewText = value
            End Set
        End Property
        Property ReviewDate() As String
            Get
                Return mReviewDate
            End Get
            Set(ByVal value As String)
                mReviewDate = value
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
        Property CustomerRate() As Double
            Get
                Return mCustomerRate
            End Get
            Set(ByVal value As Double)
                mCustomerRate = value
            End Set
        End Property
        Property Published() As Integer
            Get
                Return mPublished
            End Get
            Set(ByVal value As Integer)
                mPublished = value
            End Set
        End Property
        Property ReviewHour() As String
            Get
                Return mReviewHour
            End Get
            Set(ByVal value As String)
                mReviewHour = value
            End Set
        End Property
        Property GuestID As String
            Get
                Return mGuestID
            End Get
            Set(ByVal value As String)
                mGuestID = value
            End Set
        End Property
#End Region
#Region "Function"
        Public Function Load(ByVal sItemNo As String, ByVal nLineNo As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Customer Reviews] WHERE [Item No_] = '" & sItemNo & "' AND [Line No_] = " & nLineNo
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mLineNo_ = GetValue(SqlReader, 0, typeOfColumn.GetInt32)
                    mItemNo_ = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mCustomerNo_ = GetValue(SqlReader, 2, typeOfColumn.GetString)
                    mCustomerName = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mEmail = GetValue(SqlReader, 4, typeOfColumn.GetString)
                    mReviewTitle = GetValue(SqlReader, 5, typeOfColumn.GetString)
                    mReviewText = GetValue(SqlReader, 6, typeOfColumn.GetString)
                    mReviewDate = GetValue(SqlReader, 7, typeOfColumn.GetString)
                    mCustomerIP = GetValue(SqlReader, 8, typeOfColumn.GetString)
                    mCustomerRate = GetValue(SqlReader, 9, typeOfColumn.GetDecimal)
                    mPublished = GetValue(SqlReader, 10, typeOfColumn.GetInt32)
                    mReviewHour = GetValue(SqlReader, 11, typeOfColumn.GetString)
                    mGuestID = GetValue(SqlReader, 12, typeOfColumn.GetString)
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
        Public Function Save() As String
            Dim arParams() As IDataParameter = New IDataParameter(12) {}
            Try
                arParams(0) = DBHelper.createParameter("@LineNo_", SqlDbType.Int, ParameterDirection.Input, mLineNo_)
                arParams(1) = DBHelper.createParameter("@ItemNo_", SqlDbType.NVarChar, ParameterDirection.Input, mItemNo_)
                arParams(2) = DBHelper.createParameter("@CustomerNo_", SqlDbType.NVarChar, ParameterDirection.Input, mCustomerNo_)
                arParams(3) = DBHelper.createParameter("@CustomerName", SqlDbType.NVarChar, ParameterDirection.Input, mCustomerName)
                arParams(4) = DBHelper.createParameter("@Email", SqlDbType.NVarChar, ParameterDirection.Input, mEmail)
                arParams(5) = DBHelper.createParameter("@ReviewTitle", SqlDbType.NVarChar, ParameterDirection.Input, mReviewTitle)
                arParams(6) = DBHelper.createParameter("@ReviewText", SqlDbType.NVarChar, ParameterDirection.Input, mReviewText)
                arParams(7) = DBHelper.createParameter("@ReviewDate", SqlDbType.NVarChar, ParameterDirection.Input, mReviewDate)
                arParams(8) = DBHelper.createParameter("@CustomerIP", SqlDbType.NVarChar, ParameterDirection.Input, mCustomerIP)
                arParams(9) = DBHelper.createParameter("@CustomerRate", SqlDbType.Decimal, ParameterDirection.Input, mCustomerRate)
                arParams(10) = DBHelper.createParameter("@Published", SqlDbType.Int, ParameterDirection.Input, mPublished)
                arParams(11) = DBHelper.createParameter("@ReviewHour", SqlDbType.NVarChar, ParameterDirection.Input, mReviewHour)
                arParams(12) = DBHelper.createParameter("@GuestID", SqlDbType.NVarChar, ParameterDirection.Input, mGuestID)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_CustomerReviews", arParams)
                Return ""
            Catch ex As Exception
                Return ex.Message
            End Try
        End Function

        Function GetRating(ByVal sItemNo As String) As Integer
            Dim SQL As String
            SQL = "select ISNULL(SUM([Customer Rate])/COUNT([Line No_]),0) AS Rating from [Customer Reviews] where [Item No_] = '" & sItemNo & "' "
            Return DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL)
        End Function

        Function GetNumOfRating(ByVal sItemNo As String) As Integer
            Dim SQL As String
            SQL = "select ISNULL(COUNT([Line No_]),0) AS Rating from [Customer Reviews] where [Item No_] = '" & sItemNo & "' "
            Return DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL)
        End Function

        Function CheckHaveRated(ByVal sItemNo As String, ByVal sQuestID As String) As Boolean
            Dim SQL As String
            SQL = "SELECT ISNULL([Item No_],'') ItemNo FROM [Customer Reviews] WHERE [Item No_] = '" & sItemNo & "' AND [Guest ID] = '" & sQuestID & "'"
            Dim sValue As String = ""
            sValue = ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), "").ToString.Trim
            Return sValue.Trim = ""
        End Function

#End Region

    End Class

End Namespace