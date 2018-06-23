Imports System.Data
Imports Microsoft.VisualBasic

Namespace adv.Business
    Public Class Promotion

#Region "Khai báo"
        Private mNo_ As String = ""
        Private mName As String = ""
        Private mStartingDate As String = ""
        Private mEndingDate As String = ""
        Private mPromotionType As Integer = 0
        Private mBrandNo_ As String = ""
        Private mDivisionNo_ As String = ""
        Private mCategoryNo_ As String = ""
        Private mProductGroupNo_ As String = ""
        Private mItemNo_ As String = ""
        Private mDescription As String = ""
        Private mDateCreating As String = ""
        Private mUserID As String = ""
        Private mPublished As Integer = 0
        Private mMenuNo_ As String = ""

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
        Property PromotionType() As Integer
            Get
                Return mPromotionType
            End Get
            Set(ByVal value As Integer)
                mPromotionType = value
            End Set
        End Property
        Property BrandNo_() As String
            Get
                Return mBrandNo_
            End Get
            Set(ByVal value As String)
                mBrandNo_ = value
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
        Property ProductGroupNo_() As String
            Get
                Return mProductGroupNo_
            End Get
            Set(ByVal value As String)
                mProductGroupNo_ = value
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
        Property Description() As String
            Get
                Return mDescription
            End Get
            Set(ByVal value As String)
                mDescription = value
            End Set
        End Property
        Property DateCreating() As String
            Get
                Return mDateCreating
            End Get
            Set(ByVal value As String)
                mDateCreating = value
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
        Property Published() As Integer
            Get
                Return mPublished
            End Get
            Set(ByVal value As Integer)
                mPublished = value
            End Set
        End Property
        Property MenuNo As String
            Get
                Return mMenuNo_
            End Get
            Set(ByVal value As String)
                mMenuNo_ = value
            End Set
        End Property
#End Region
#Region "Function"
        Public Function Load(ByVal sNo As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Promotions] WHERE No_ = '" & sNo & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mName = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mStartingDate = GetValue(SqlReader, 2, typeOfColumn.GetString)
                    mEndingDate = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mPromotionType = GetValue(SqlReader, 4, typeOfColumn.GetInt32)
                    mBrandNo_ = GetValue(SqlReader, 5, typeOfColumn.GetString)
                    mDivisionNo_ = GetValue(SqlReader, 6, typeOfColumn.GetString)
                    mCategoryNo_ = GetValue(SqlReader, 7, typeOfColumn.GetString)
                    mProductGroupNo_ = GetValue(SqlReader, 8, typeOfColumn.GetString)
                    mItemNo_ = GetValue(SqlReader, 9, typeOfColumn.GetString)
                    mDescription = GetValue(SqlReader, 10, typeOfColumn.GetString)
                    mDateCreating = GetValue(SqlReader, 11, typeOfColumn.GetString)
                    mUserID = GetValue(SqlReader, 12, typeOfColumn.GetString)
                    mPublished = GetValue(SqlReader, 13, typeOfColumn.GetInt32)
                    mMenuNo_ = GetValue(SqlReader, 14, typeOfColumn.GetString)
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
            Dim arParams() As IDataParameter = New IDataParameter(14) {}
            Try
                arParams(0) = DBHelper.createParameter("@No_", SqlDbType.NVarChar, ParameterDirection.Input, mNo_)
                arParams(1) = DBHelper.createParameter("@Name", SqlDbType.NVarChar, ParameterDirection.Input, mName)
                arParams(2) = DBHelper.createParameter("@StartingDate", SqlDbType.NVarChar, ParameterDirection.Input, mStartingDate)
                arParams(3) = DBHelper.createParameter("@EndingDate", SqlDbType.NVarChar, ParameterDirection.Input, mEndingDate)
                arParams(4) = DBHelper.createParameter("@PromotionType", SqlDbType.Int, ParameterDirection.Input, mPromotionType)
                arParams(5) = DBHelper.createParameter("@BrandNo_", SqlDbType.NVarChar, ParameterDirection.Input, mBrandNo_)
                arParams(6) = DBHelper.createParameter("@DivisionNo_", SqlDbType.NVarChar, ParameterDirection.Input, mDivisionNo_)
                arParams(7) = DBHelper.createParameter("@CategoryNo_", SqlDbType.NVarChar, ParameterDirection.Input, mCategoryNo_)
                arParams(8) = DBHelper.createParameter("@ProductGroupNo_", SqlDbType.NVarChar, ParameterDirection.Input, mProductGroupNo_)
                arParams(9) = DBHelper.createParameter("@ItemNo_", SqlDbType.NVarChar, ParameterDirection.Input, mItemNo_)
                arParams(10) = DBHelper.createParameter("@Description", SqlDbType.NVarChar, ParameterDirection.Input, mDescription)
                arParams(11) = DBHelper.createParameter("@DateCreating", SqlDbType.NVarChar, ParameterDirection.Input, mDateCreating)
                arParams(12) = DBHelper.createParameter("@UserID", SqlDbType.NVarChar, ParameterDirection.Input, mUserID)
                arParams(13) = DBHelper.createParameter("@Published", SqlDbType.Int, ParameterDirection.Input, mPublished)
                arParams(14) = DBHelper.createParameter("@MenuNo_", SqlDbType.NVarChar, ParameterDirection.Input, mMenuNo_)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_Promotions", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
#End Region

    End Class

End Namespace