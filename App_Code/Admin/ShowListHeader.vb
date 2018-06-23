Imports Microsoft.VisualBasic
Imports System.Data

Namespace adv.Business
    Public Class ShowListHeader
#Region "Khai báo"
        Private mNo_ As String = ""
        Private mTitle As String = ""
        Private mStartingDate As String = ""
        Private mEndingDate As String = ""
        Private mPageNo_ As String = ""
        Private mPublished As Integer = 0
        Private mUserID As String = ""
        Private mImgURL As String = ""
        Private mContentType As Integer = 0
        Private mPositionNo_ As String = ""
        Private mAreaCode As String = ""
        Private mListType As Integer = 0
        Private mUrlPage As String = ""
        Private mMenuNo_ As String = ""
        Private mNumItemAtHome As Integer = 0
        Private mPositionOrder As Integer = 0

        Property No_() As String
            Get
                Return mNo_
            End Get
            Set(ByVal value As String)
                mNo_ = value
            End Set
        End Property
        Property Title() As String
            Get
                Return mTitle
            End Get
            Set(ByVal value As String)
                mTitle = value
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
        Property PageNo_() As String
            Get
                Return mPageNo_
            End Get
            Set(ByVal value As String)
                mPageNo_ = value
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
        Property UserID() As String
            Get
                Return mUserID
            End Get
            Set(ByVal value As String)
                mUserID = value
            End Set
        End Property
        Property ImgURL() As String
            Get
                Return mImgURL
            End Get
            Set(ByVal value As String)
                mImgURL = value
            End Set
        End Property
        Property ContentType() As Integer
            Get
                Return mContentType
            End Get
            Set(ByVal value As Integer)
                mContentType = value
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
        Property AreaCode() As String
            Get
                Return mAreaCode
            End Get
            Set(ByVal value As String)
                mAreaCode = value
            End Set
        End Property
        Property ListType() As Integer
            Get
                Return mListType
            End Get
            Set(ByVal value As Integer)
                mListType = value
            End Set
        End Property
        Property UrlPage() As String
            Get
                Return mUrlPage
            End Get
            Set(ByVal value As String)
                mUrlPage = value
            End Set
        End Property
        Property MenuNo_() As String
            Get
                Return mMenuNo_
            End Get
            Set(ByVal value As String)
                mMenuNo_ = value
            End Set
        End Property
        Property NumItemAtHome() As Integer
            Get
                Return mNumItemAtHome
            End Get
            Set(ByVal value As Integer)
                mNumItemAtHome = value
            End Set
        End Property
        Property PositionOrder() As Integer
            Get
                Return mPositionOrder
            End Get
            Set(ByVal value As Integer)
                mPositionOrder = value
            End Set
        End Property

#End Region
#Region "Function"
        Public Function Load(ByVal sNo As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Show List Header] WHERE No_ = '" & sNo & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mTitle = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mStartingDate = GetValue(SqlReader, 2, typeOfColumn.GetString)
                    mEndingDate = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mPageNo_ = GetValue(SqlReader, 4, typeOfColumn.GetString)
                    mPublished = GetValue(SqlReader, 5, typeOfColumn.GetInt32)
                    mUserID = GetValue(SqlReader, 6, typeOfColumn.GetString)
                    mImgURL = GetValue(SqlReader, 7, typeOfColumn.GetString)
                    mContentType = GetValue(SqlReader, 8, typeOfColumn.GetInt32)
                    mPositionNo_ = GetValue(SqlReader, 9, typeOfColumn.GetString)
                    mAreaCode = GetValue(SqlReader, 10, typeOfColumn.GetString)
                    mListType = GetValue(SqlReader, 11, typeOfColumn.GetInt32)
                    mUrlPage = GetValue(SqlReader, 12, typeOfColumn.GetString)
                    mMenuNo_ = GetValue(SqlReader, 13, typeOfColumn.GetString)
                    mNumItemAtHome = GetValue(SqlReader, 14, typeOfColumn.GetInt32)
                    mPositionOrder = GetValue(SqlReader, 15, typeOfColumn.GetInt32)
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
            Dim arParams() As IDataParameter = New IDataParameter(15) {}
            Try
                arParams(0) = DBHelper.createParameter("@No_", SqlDbType.NVarChar, ParameterDirection.Input, mNo_)
                arParams(1) = DBHelper.createParameter("@Title", SqlDbType.NVarChar, ParameterDirection.Input, mTitle)
                arParams(2) = DBHelper.createParameter("@StartingDate", SqlDbType.NVarChar, ParameterDirection.Input, mStartingDate)
                arParams(3) = DBHelper.createParameter("@EndingDate", SqlDbType.NVarChar, ParameterDirection.Input, mEndingDate)
                arParams(4) = DBHelper.createParameter("@PageNo_", SqlDbType.NVarChar, ParameterDirection.Input, mPageNo_)
                arParams(5) = DBHelper.createParameter("@Published", SqlDbType.Int, ParameterDirection.Input, mPublished)
                arParams(6) = DBHelper.createParameter("@UserID", SqlDbType.NVarChar, ParameterDirection.Input, mUserID)
                arParams(7) = DBHelper.createParameter("@ImgURL", SqlDbType.NVarChar, ParameterDirection.Input, mImgURL)
                arParams(8) = DBHelper.createParameter("@ContentType", SqlDbType.Int, ParameterDirection.Input, mContentType)
                arParams(9) = DBHelper.createParameter("@PositionNo_", SqlDbType.NVarChar, ParameterDirection.Input, mPositionNo_)
                arParams(10) = DBHelper.createParameter("@AreaCode", SqlDbType.NVarChar, ParameterDirection.Input, mAreaCode)
                arParams(11) = DBHelper.createParameter("@ListType", SqlDbType.Int, ParameterDirection.Input, mListType)
                arParams(12) = DBHelper.createParameter("@UrlPage", SqlDbType.NVarChar, ParameterDirection.Input, mUrlPage)
                arParams(13) = DBHelper.createParameter("@MenuNo_", SqlDbType.NVarChar, ParameterDirection.Input, mMenuNo_)
                arParams(14) = DBHelper.createParameter("@NumItemAtHome", SqlDbType.Int, ParameterDirection.Input, mNumItemAtHome)
                arParams(15) = DBHelper.createParameter("@PositionOrder", SqlDbType.Int, ParameterDirection.Input, mPositionOrder)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_ShowListHeader", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
#End Region

    End Class
End Namespace