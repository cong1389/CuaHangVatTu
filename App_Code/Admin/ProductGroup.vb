Imports System.Data
Imports Microsoft.VisualBasic

Namespace adv.Business

    Public Class ProductGroup
#Region "Khai báo"
        Private mNo_ As String = ""
        Private mName As String = ""
        Private mType As Integer = 0
        Private mDivisionNo_ As String = ""
        Private mCategoryNo_ As String = ""
        Private mMenuGroupNo_ As String = ""
        Private mLastDateModify As String = ""
        Private mUserID As String = ""
        Private mMenuDivisionNo_ As String = ""
        Private mMenuCategoryNo_ As String = ""
        Private mPageNo_ As String = ""

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
        Property Type() As Integer
            Get
                Return mType
            End Get
            Set(ByVal value As Integer)
                mType = value
            End Set
        End Property
        Property DivisionNo_() As String
            Get
                Return mDivisionNo_
            End Get
            Set(ByVal value As String)
                mDivisionNo_ = value
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
        Property MenuGroupNo_() As String
            Get
                Return mMenuGroupNo_
            End Get
            Set(ByVal value As String)
                mMenuGroupNo_ = value
            End Set
        End Property
        Property LastDateModify() As String
            Get
                Return mLastDateModify
            End Get
            Set(ByVal value As String)
                mLastDateModify = value
            End Set
        End Property
        Property UserID() As String
            Get
                Return mUserID
            End Get
            Set(ByVal value As String)
                mUserID = value
            End Set
        End Property
        Property MenuDivisionNo_() As String
            Get
                Return mMenuDivisionNo_
            End Get
            Set(ByVal value As String)
                mMenuDivisionNo_ = value
            End Set
        End Property
        Property MenuCategoryNo_() As String
            Get
                Return mMenuCategoryNo_
            End Get
            Set(ByVal value As String)
                mMenuCategoryNo_ = value
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

#End Region
#Region "Function"
        Public Function Load(ByVal sNo As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Product Group] WHERE No_ = '" & sNo & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mName = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mType = GetValue(SqlReader, 2, typeOfColumn.GetInt32)
                    mDivisionNo_ = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mCategoryNo_ = GetValue(SqlReader, 4, typeOfColumn.GetString)
                    mMenuGroupNo_ = GetValue(SqlReader, 5, typeOfColumn.GetString)
                    mLastDateModify = GetValue(SqlReader, 6, typeOfColumn.GetString)
                    mUserID = GetValue(SqlReader, 7, typeOfColumn.GetString)
                    mMenuDivisionNo_ = GetValue(SqlReader, 8, typeOfColumn.GetString)
                    mMenuCategoryNo_ = GetValue(SqlReader, 9, typeOfColumn.GetString)
                    mPageNo_ = GetValue(SqlReader, 10, typeOfColumn.GetString)
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

        Public Function Save(ByVal sOldNo As String) As Boolean
            Dim arParams() As IDataParameter = New IDataParameter(11) {}
            Try
                arParams(0) = DBHelper.createParameter("@No_", SqlDbType.NVarChar, ParameterDirection.Input, mNo_)
                arParams(1) = DBHelper.createParameter("@Name", SqlDbType.NVarChar, ParameterDirection.Input, mName)
                arParams(2) = DBHelper.createParameter("@Type", SqlDbType.Int, ParameterDirection.Input, mType)
                arParams(3) = DBHelper.createParameter("@DivisionNo_", SqlDbType.NVarChar, ParameterDirection.Input, mDivisionNo_)
                arParams(4) = DBHelper.createParameter("@CategoryNo_", SqlDbType.NVarChar, ParameterDirection.Input, mCategoryNo_)
                arParams(5) = DBHelper.createParameter("@MenuGroupNo_", SqlDbType.NVarChar, ParameterDirection.Input, mMenuGroupNo_)
                arParams(6) = DBHelper.createParameter("@LastDateModify", SqlDbType.NVarChar, ParameterDirection.Input, mLastDateModify)
                arParams(7) = DBHelper.createParameter("@UserID", SqlDbType.NVarChar, ParameterDirection.Input, mUserID)
                arParams(8) = DBHelper.createParameter("@MenuDivisionNo_", SqlDbType.NVarChar, ParameterDirection.Input, mMenuDivisionNo_)
                arParams(9) = DBHelper.createParameter("@MenuCategoryNo_", SqlDbType.NVarChar, ParameterDirection.Input, mMenuCategoryNo_)
                arParams(10) = DBHelper.createParameter("@PageNo_", SqlDbType.NVarChar, ParameterDirection.Input, mPageNo_)
                arParams(11) = DBHelper.createParameter("@OldNo_", SqlDbType.NVarChar, ParameterDirection.Input, sOldNo)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_ProductGroup", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
#End Region

    End Class

End Namespace