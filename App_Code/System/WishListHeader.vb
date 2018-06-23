Imports Microsoft.VisualBasic
Imports System.Data

Namespace adv.Business
    Public Class WishListHeader
#Region "Khai báo"
        Private mNo_ As String = ""
        Private mName As String = ""
        Private mCustomerNo_ As String = ""
        Private mDateCreated As String = ""
        Private mDateModify As String = ""
        Private mRolesType As Integer = 0
        Private mDescriptions As String = ""
        Private mPageNo_ As String = ""
        Private mHomeoneGuestNo_ As String = ""

        Property No_() As String
            Get
                Return mNo_
            End Get
            Set(ByVal value As String)
                mNo_ = value
            End Set
        End Property
        Property Name() As String
            Get
                Return mName
            End Get
            Set(ByVal value As String)
                mName = value
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
        Property DateCreated() As String
            Get
                Return mDateCreated
            End Get
            Set(ByVal value As String)
                mDateCreated = value
            End Set
        End Property
        Property DateModify() As String
            Get
                Return mDateModify
            End Get
            Set(ByVal value As String)
                mDateModify = value
            End Set
        End Property
        Property RolesType() As Integer
            Get
                Return mRolesType
            End Get
            Set(ByVal value As Integer)
                mRolesType = value
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
        Property PageNo_() As String
            Get
                Return mPageNo_
            End Get
            Set(ByVal value As String)
                mPageNo_ = value
            End Set
        End Property
        Property HomeoneGuestNo_() As String
            Get
                Return mHomeoneGuestNo_
            End Get
            Set(ByVal value As String)
                mHomeoneGuestNo_ = value
            End Set
        End Property

#End Region
#Region "Function"
        Public Function Load(ByVal sNo As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Wishlist Header] WHERE No_ = '" & sNo & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mName = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mCustomerNo_ = GetValue(SqlReader, 2, typeOfColumn.GetString)
                    mDateCreated = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mDateModify = GetValue(SqlReader, 4, typeOfColumn.GetString)
                    mRolesType = GetValue(SqlReader, 5, typeOfColumn.GetInt32)
                    mDescriptions = GetValue(SqlReader, 6, typeOfColumn.GetString)
                    mPageNo_ = GetValue(SqlReader, 7, typeOfColumn.GetString)
                    mHomeoneGuestNo_ = GetValue(SqlReader, 8, typeOfColumn.GetString)
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
            Dim arParams() As IDataParameter = New IDataParameter(8) {}
            Try
                arParams(0) = DBHelper.createParameter("@No_", SqlDbType.NVarChar, ParameterDirection.Input, mNo_)
                arParams(1) = DBHelper.createParameter("@Name", SqlDbType.NVarChar, ParameterDirection.Input, mName)
                arParams(2) = DBHelper.createParameter("@CustomerNo_", SqlDbType.NVarChar, ParameterDirection.Input, mCustomerNo_)
                arParams(3) = DBHelper.createParameter("@DateCreated", SqlDbType.NVarChar, ParameterDirection.Input, mDateCreated)
                arParams(4) = DBHelper.createParameter("@DateModify", SqlDbType.NVarChar, ParameterDirection.Input, mDateModify)
                arParams(5) = DBHelper.createParameter("@RolesType", SqlDbType.Int, ParameterDirection.Input, mRolesType)
                arParams(6) = DBHelper.createParameter("@Descriptions", SqlDbType.NVarChar, ParameterDirection.Input, mDescriptions)
                arParams(7) = DBHelper.createParameter("@PageNo_", SqlDbType.NVarChar, ParameterDirection.Input, mPageNo_)
                arParams(8) = DBHelper.createParameter("@HomeoneGuestNo_", SqlDbType.NVarChar, ParameterDirection.Input, mHomeoneGuestNo_)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_WishlistHeader", arParams)
                Return True
            Catch ex As Exception

                Return False
            End Try
        End Function
#End Region

    End Class
End Namespace
