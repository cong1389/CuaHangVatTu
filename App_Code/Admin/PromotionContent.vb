Imports System.Data
Imports Microsoft.VisualBasic

Namespace adv.Business
    Public Class PromotionContent
#Region "Khai báo"
        Private mNo_ As String = ""
        Private mTitle As String = ""
        Private mStartingDate As String = ""
        Private mEndingDate As String = ""
        Private mDateCreated As String = ""
        Private mUserID As String = ""
        Private mLevel As Integer = 0
        Private mPublished As Integer = 0
        Private mPageBackground As String = ""
        Private mContent As String = ""
        Private mLinkContent As String = ""

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
        Property DateCreated() As String
            Get
                Return mDateCreated
            End Get
            Set(ByVal value As String)
                mDateCreated = value
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
        Property Level() As Integer
            Get
                Return mLevel
            End Get
            Set(ByVal value As Integer)
                mLevel = value
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
        Property PageBackground() As String
            Get
                Return mPageBackground
            End Get
            Set(ByVal value As String)
                mPageBackground = value
            End Set
        End Property
        Property Content() As String
            Get
                Return mContent
            End Get
            Set(ByVal value As String)
                mContent = value
            End Set
        End Property
        Property LinkContent() As String
            Get
                Return mLinkContent
            End Get
            Set(ByVal value As String)
                mLinkContent = value
            End Set
        End Property
#End Region
#Region "Function"
        Public Function Load(ByVal sNo As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Promotions Content] WHERE No_ = '" & sNo & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mTitle = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mStartingDate = GetValue(SqlReader, 2, typeOfColumn.GetString)
                    mEndingDate = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mDateCreated = GetValue(SqlReader, 4, typeOfColumn.GetString)
                    mUserID = GetValue(SqlReader, 5, typeOfColumn.GetString)
                    mLevel = GetValue(SqlReader, 6, typeOfColumn.GetInt32)
                    mPublished = GetValue(SqlReader, 7, typeOfColumn.GetInt32)
                    mPageBackground = GetValue(SqlReader, 8, typeOfColumn.GetString)
                    mContent = GetValue(SqlReader, 9, typeOfColumn.GetString)
                    mLinkContent = GetValue(SqlReader, 10, typeOfColumn.GetString)
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
        Public Function LoadByLink(ByVal sLink As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Promotions Content] WHERE [Link Content] = '" & sLink & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mTitle = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mStartingDate = GetValue(SqlReader, 2, typeOfColumn.GetString)
                    mEndingDate = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mDateCreated = GetValue(SqlReader, 4, typeOfColumn.GetString)
                    mUserID = GetValue(SqlReader, 5, typeOfColumn.GetString)
                    mLevel = GetValue(SqlReader, 6, typeOfColumn.GetInt32)
                    mPublished = GetValue(SqlReader, 7, typeOfColumn.GetInt32)
                    mPageBackground = GetValue(SqlReader, 8, typeOfColumn.GetString)
                    mContent = GetValue(SqlReader, 9, typeOfColumn.GetString)
                    mLinkContent = GetValue(SqlReader, 10, typeOfColumn.GetString)
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
            Dim arParams() As IDataParameter = New IDataParameter(10) {}
            Try
                arParams(0) = DBHelper.createParameter("@No_", SqlDbType.NVarChar, ParameterDirection.Input, mNo_)
                arParams(1) = DBHelper.createParameter("@Title", SqlDbType.NVarChar, ParameterDirection.Input, mTitle)
                arParams(2) = DBHelper.createParameter("@StartingDate", SqlDbType.NVarChar, ParameterDirection.Input, mStartingDate)
                arParams(3) = DBHelper.createParameter("@EndingDate", SqlDbType.NVarChar, ParameterDirection.Input, mEndingDate)
                arParams(4) = DBHelper.createParameter("@DateCreated", SqlDbType.NVarChar, ParameterDirection.Input, mDateCreated)
                arParams(5) = DBHelper.createParameter("@UserID", SqlDbType.NVarChar, ParameterDirection.Input, mUserID)
                arParams(6) = DBHelper.createParameter("@Level", SqlDbType.Int, ParameterDirection.Input, mLevel)
                arParams(7) = DBHelper.createParameter("@Published", SqlDbType.Int, ParameterDirection.Input, mPublished)
                arParams(8) = DBHelper.createParameter("@PageBackground", SqlDbType.NVarChar, ParameterDirection.Input, mPageBackground)
                arParams(9) = DBHelper.createParameter("@Content", SqlDbType.NVarChar, ParameterDirection.Input, mContent)
                arParams(10) = DBHelper.createParameter("@LinkContent", SqlDbType.NVarChar, ParameterDirection.Input, mLinkContent)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_PromotionContent", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
#End Region

    End Class
End Namespace