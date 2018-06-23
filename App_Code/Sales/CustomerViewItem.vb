Imports Microsoft.VisualBasic
Imports System.Data

Namespace adv.Business
    Public Class CustomerViewItem
#Region "Khai báo"
        Private mCookieKey As String = ""
        Private mItemNo_ As String = ""
        Private mDateView As String = ""
        Private mIPNo_ As String = ""
        Private mCustomerNo_ As String = ""

        Property CookieKey() As String
            Get
                Return mCookieKey
            End Get
            Set(ByVal value As String)
                mCookieKey = value
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
        Property DateView() As String
            Get
                Return mDateView
            End Get
            Set(ByVal value As String)
                mDateView = value
            End Set
        End Property
        Property IPNo_() As String
            Get
                Return mIPNo_
            End Get
            Set(ByVal value As String)
                mIPNo_ = value
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

#End Region
#Region "Function"
        Public Function Load(ByVal sCookieKey As String, ByVal sItemNo As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Customer View Item] WHERE [Cookie Key] = '" & sCookieKey & "' AND [Item No_] = '" & sItemNo & "'"
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mCookieKey = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mItemNo_ = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mDateView = GetValue(SqlReader, 2, typeOfColumn.GetString)
                    mIPNo_ = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mCustomerNo_ = GetValue(SqlReader, 4, typeOfColumn.GetString)
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
            Dim arParams() As IDataParameter = New IDataParameter(4) {}
            Try
                arParams(0) = DBHelper.createParameter("@CookieKey", SqlDbType.NVarChar, ParameterDirection.Input, mCookieKey)
                arParams(1) = DBHelper.createParameter("@ItemNo_", SqlDbType.NVarChar, ParameterDirection.Input, mItemNo_)
                arParams(2) = DBHelper.createParameter("@DateView", SqlDbType.NVarChar, ParameterDirection.Input, mDateView)
                arParams(3) = DBHelper.createParameter("@IPNo_", SqlDbType.NVarChar, ParameterDirection.Input, mIPNo_)
                arParams(4) = DBHelper.createParameter("@CustomerNo_", SqlDbType.NVarChar, ParameterDirection.Input, mCustomerNo_)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_CustomerViewItem", arParams)
                Return True
            Catch ex As Exception

                Return False
            End Try
        End Function
#End Region

    End Class
End Namespace