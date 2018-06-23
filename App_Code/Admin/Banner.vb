Imports System.Data
Imports Microsoft.VisualBasic

Namespace adv.Business
    Public Class Banner
#Region "Khai báo"
        Private mNo_ As String = ""
        Private mName As String = ""
        Private mPositionNo_ As String = ""
        Private mImagesSrc As String = ""
        Private mStartingDate As String = ""
        Private mEndingDate As String = ""
        Private mUrl As String = ""
        Private mNumClick As Integer = 0
        Private mShow As Integer = 0
        Private mOrderPosition As Integer = 0
        Private mGoodMenuNo_ As String = ""
        Private mGoodMenuType As Integer = 0
        Private mBannerGroupNo_ As String = ""
        Private mNewWindows As Integer = 0
        Private mWidth As Integer = 0
        Private mHeight As Integer = 0
        Private mPage As String = ""
        Private mIsRun As Integer = 0


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
        Property PositionNo_() As String
            Get
                Return mPositionNo_
            End Get
            Set(ByVal value As String)
                mPositionNo_ = value
            End Set
        End Property
        Property ImagesSrc() As String
            Get
                Return mImagesSrc
            End Get
            Set(ByVal value As String)
                mImagesSrc = value
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
        Property Url() As String
            Get
                Return mUrl
            End Get
            Set(ByVal value As String)
                mUrl = value
            End Set
        End Property
        Property NumClick() As Integer
            Get
                Return mNumClick
            End Get
            Set(ByVal value As Integer)
                mNumClick = value
            End Set
        End Property
        Property Show() As Integer
            Get
                Return mShow
            End Get
            Set(ByVal value As Integer)
                mShow = value
            End Set
        End Property
        Property OrderPosition() As Integer
            Get
                Return mOrderPosition
            End Get
            Set(ByVal value As Integer)
                mOrderPosition = value
            End Set
        End Property
        Property GoodMenuNo_() As String
            Get
                Return mGoodMenuNo_
            End Get
            Set(ByVal value As String)
                mGoodMenuNo_ = value
            End Set
        End Property
        Property GoodMenuType() As Integer
            Get
                Return mGoodMenuType
            End Get
            Set(ByVal value As Integer)
                mGoodMenuType = value
            End Set
        End Property
        Property BannerGroupNo_() As String
            Get
                Return mBannerGroupNo_
            End Get
            Set(ByVal value As String)
                mBannerGroupNo_ = value
            End Set
        End Property
        Property NewWindows() As Integer
            Get
                Return mNewWindows
            End Get
            Set(ByVal value As Integer)
                mNewWindows = value
            End Set
        End Property
        Property Width() As Integer
            Get
                Return mWidth
            End Get
            Set(ByVal value As Integer)
                mWidth = value
            End Set
        End Property
        Property Height() As Integer
            Get
                Return mHeight
            End Get
            Set(ByVal value As Integer)
                mHeight = value
            End Set
        End Property
        Property Page As String
            Get
                Return mPage
            End Get
            Set(ByVal value As String)
                mPage = value
            End Set
        End Property
        Property IsRun As Integer
            Get
                Return mIsRun
            End Get
            Set(ByVal value As Integer)
                mIsRun = value
            End Set
        End Property
#End Region
#Region "Function"
        Public Function Load(ByVal sNo As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Banner] WHERE No_ = '" & sNo & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mName = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mPositionNo_ = GetValue(SqlReader, 2, typeOfColumn.GetString)
                    mImagesSrc = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mStartingDate = GetValue(SqlReader, 4, typeOfColumn.GetString)
                    mEndingDate = GetValue(SqlReader, 5, typeOfColumn.GetString)
                    mUrl = GetValue(SqlReader, 6, typeOfColumn.GetString)
                    mNumClick = GetValue(SqlReader, 7, typeOfColumn.GetInt32)
                    mShow = GetValue(SqlReader, 8, typeOfColumn.GetInt32)
                    mOrderPosition = GetValue(SqlReader, 9, typeOfColumn.GetInt32)
                    mGoodMenuNo_ = GetValue(SqlReader, 10, typeOfColumn.GetString)
                    mGoodMenuType = GetValue(SqlReader, 11, typeOfColumn.GetInt32)
                    mBannerGroupNo_ = GetValue(SqlReader, 12, typeOfColumn.GetString)
                    mNewWindows = GetValue(SqlReader, 13, typeOfColumn.GetInt32)
                    mWidth = GetValue(SqlReader, 14, typeOfColumn.GetInt32)
                    mHeight = GetValue(SqlReader, 15, typeOfColumn.GetInt32)
                    mPage = GetValue(SqlReader, 16, typeOfColumn.GetString)
                    mIsRun = GetValue(SqlReader, 17, typeOfColumn.GetInt32)
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
            Dim arParams() As IDataParameter = New IDataParameter(17) {}
            Try
                arParams(0) = DBHelper.createParameter("@No_", SqlDbType.NVarChar, ParameterDirection.Input, mNo_)
                arParams(1) = DBHelper.createParameter("@Name", SqlDbType.NVarChar, ParameterDirection.Input, mName)
                arParams(2) = DBHelper.createParameter("@PositionNo_", SqlDbType.NVarChar, ParameterDirection.Input, mPositionNo_)
                arParams(3) = DBHelper.createParameter("@ImagesSrc", SqlDbType.NVarChar, ParameterDirection.Input, mImagesSrc)
                arParams(4) = DBHelper.createParameter("@StartingDate", SqlDbType.NVarChar, ParameterDirection.Input, mStartingDate)
                arParams(5) = DBHelper.createParameter("@EndingDate", SqlDbType.NVarChar, ParameterDirection.Input, mEndingDate)
                arParams(6) = DBHelper.createParameter("@Url", SqlDbType.NVarChar, ParameterDirection.Input, mUrl)
                arParams(7) = DBHelper.createParameter("@NumClick", SqlDbType.Int, ParameterDirection.Input, mNumClick)
                arParams(8) = DBHelper.createParameter("@Show", SqlDbType.Int, ParameterDirection.Input, mShow)
                arParams(9) = DBHelper.createParameter("@OrderPosition", SqlDbType.Int, ParameterDirection.Input, mOrderPosition)
                arParams(10) = DBHelper.createParameter("@GoodMenuNo_", SqlDbType.NVarChar, ParameterDirection.Input, mGoodMenuNo_)
                arParams(11) = DBHelper.createParameter("@GoodMenuType", SqlDbType.Int, ParameterDirection.Input, mGoodMenuType)
                arParams(12) = DBHelper.createParameter("@BannerGroupNo_", SqlDbType.NVarChar, ParameterDirection.Input, mBannerGroupNo_)
                arParams(13) = DBHelper.createParameter("@NewWindows", SqlDbType.Int, ParameterDirection.Input, mNewWindows)
                arParams(14) = DBHelper.createParameter("@Width", SqlDbType.Int, ParameterDirection.Input, mWidth)
                arParams(15) = DBHelper.createParameter("@Height", SqlDbType.Int, ParameterDirection.Input, mHeight)
                arParams(16) = DBHelper.createParameter("@Page", SqlDbType.NVarChar, ParameterDirection.Input, mPage)
                arParams(17) = DBHelper.createParameter("@IsRun", SqlDbType.Int, ParameterDirection.Input, mIsRun)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_Banner", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Sub SetBlank()
            mNo_ = ""
            mName = ""
            mPositionNo_ = ""
            mImagesSrc = ""
            mStartingDate = ""
            mEndingDate = ""
            mUrl = ""
            mNumClick = 0
            mShow = 0
            mOrderPosition = 0
            mGoodMenuNo_ = ""
            mGoodMenuType = 0
            mBannerGroupNo_ = ""
            mNewWindows = 0
            mWidth = 0
            mHeight = 0
            mIsRun = 0
        End Sub

        Sub Click(ByVal sNo As String)
            Dim SQL As String
            SQL = " UPDATE Banner SET [Num Click] = ISNULL([Num Click],0) + 1 WHERE No_ = '" & sNo & "'"
            DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
        End Sub

#End Region

    End Class
End Namespace