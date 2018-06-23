Imports Microsoft.VisualBasic
Imports System.Data

Namespace adv.Business

    Public Class CustomerAddress
#Region "Khai báo"
        Private mCustomerNo_ As String = ""
        Private mLineNo_ As Integer = 0
        Private mCityNo_ As String = ""
        Private mDistrictNo_ As String = ""
        Private mWardNo_ As String = ""
        Private mHouseNo_ As String = ""
        Private mFullAddress As String = ""

        Property CustomerNo_() As String
            Get
                Return mCustomerNo_
            End Get
            Set(ByVal value As String)
                mCustomerNo_ = value
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
        Property CityNo_() As String
            Get
                Return mCityNo_
            End Get
            Set(ByVal value As String)
                mCityNo_ = value
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
        Property WardNo_() As String
            Get
                Return mWardNo_
            End Get
            Set(ByVal value As String)
                mWardNo_ = value
            End Set
        End Property
        Property HouseNo_() As String
            Get
                Return mHouseNo_
            End Get
            Set(ByVal value As String)
                mHouseNo_ = value
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

#End Region
#Region "Function"
        Public Function Load(ByVal sCustomerNo As String, ByVal nLineNo As Integer) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Customer Address] WHERE [Customer No_] = '" & sCustomerNo & "' AND [Line No_] = " & nLineNo
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mCustomerNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mLineNo_ = GetValue(SqlReader, 1, typeOfColumn.GetInt32)
                    mCityNo_ = GetValue(SqlReader, 2, typeOfColumn.GetString)
                    mDistrictNo_ = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mWardNo_ = GetValue(SqlReader, 4, typeOfColumn.GetString)
                    mHouseNo_ = GetValue(SqlReader, 5, typeOfColumn.GetString)
                    mFullAddress = GetValue(SqlReader, 6, typeOfColumn.GetString)
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
            Dim arParams() As IDataParameter = New IDataParameter(6) {}
            Try
                arParams(0) = DBHelper.createParameter("@CustomerNo_", SqlDbType.NVarChar, ParameterDirection.Input, mCustomerNo_)
                arParams(1) = DBHelper.createParameter("@LineNo_", SqlDbType.Int, ParameterDirection.Input, mLineNo_)
                arParams(2) = DBHelper.createParameter("@CityNo_", SqlDbType.NVarChar, ParameterDirection.Input, mCityNo_)
                arParams(3) = DBHelper.createParameter("@DistrictNo_", SqlDbType.NVarChar, ParameterDirection.Input, mDistrictNo_)
                arParams(4) = DBHelper.createParameter("@WardNo_", SqlDbType.NVarChar, ParameterDirection.Input, mWardNo_)
                arParams(5) = DBHelper.createParameter("@HouseNo_", SqlDbType.NVarChar, ParameterDirection.Input, mHouseNo_)
                arParams(6) = DBHelper.createParameter("@FullAddress", SqlDbType.NVarChar, ParameterDirection.Input, mFullAddress)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_CustomerAddress", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
#End Region

    End Class

End Namespace