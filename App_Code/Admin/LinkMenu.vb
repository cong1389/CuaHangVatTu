Imports Microsoft.VisualBasic
Imports System.Data

Namespace adv.Business
    Public Class LinkMenu

#Region "Khai báo"
        Private mNo_ As String = ""
        Private mName As String = ""
        Private mPositionNo_ As String = ""
        Private mGroupNo_ As String = ""
        Private mURLLink As String = ""
        Private mClassCSS As String = ""
        Private mFloat As Integer = 0
        Private mImgsrc As String = ""
        Private mLinkType As Integer = 0
        Private mOrderPosition As Integer = 0
        Private mGoodMenuNo_ As String = ""
        Private mGoodMenuOrder As Integer = 0
        Private mIsGroup As Integer = 0
        Private mParentNo_ As String = ""
        Private mLoginStatus As Integer = 0

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
        Property GroupNo_() As String
            Get
                Return mGroupNo_
            End Get
            Set(ByVal value As String)
                mGroupNo_ = value
            End Set
        End Property
        Property URLLink() As String
            Get
                Return mURLLink
            End Get
            Set(ByVal value As String)
                mURLLink = value
            End Set
        End Property
        Property ClassCSS() As String
            Get
                Return mClassCSS
            End Get
            Set(ByVal value As String)
                mClassCSS = value
            End Set
        End Property
        Property Float() As Integer
            Get
                Return mFloat
            End Get
            Set(ByVal value As Integer)
                mFloat = value
            End Set
        End Property
        Property Imgsrc() As String
            Get
                Return mImgsrc
            End Get
            Set(ByVal value As String)
                mImgsrc = value
            End Set
        End Property
        Property LinkType() As Integer
            Get
                Return mLinkType
            End Get
            Set(ByVal value As Integer)
                mLinkType = value
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
        Property GoodMenuOrder() As Integer
            Get
                Return mGoodMenuOrder
            End Get
            Set(ByVal value As Integer)
                mGoodMenuOrder = value
            End Set
        End Property
        Property IsGroup() As Integer
            Get
                Return mIsGroup
            End Get
            Set(ByVal value As Integer)
                mIsGroup = value
            End Set
        End Property
        Property ParentNo_() As String
            Get
                Return mParentNo_
            End Get
            Set(ByVal value As String)
                mParentNo_ = value
            End Set
        End Property
        Property LoginStatus As Integer
            Get
                Return mLoginStatus
            End Get
            Set(value As Integer)
                mLoginStatus = value
            End Set
        End Property
#End Region
#Region "Function"
        Public Function Load(ByVal sNo As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Link Menu] WHERE No_ = '" & sNo & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mName = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mPositionNo_ = GetValue(SqlReader, 2, typeOfColumn.GetString)
                    mGroupNo_ = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mURLLink = GetValue(SqlReader, 4, typeOfColumn.GetString)
                    mClassCSS = GetValue(SqlReader, 5, typeOfColumn.GetString)
                    mFloat = GetValue(SqlReader, 6, typeOfColumn.GetInt32)
                    mImgsrc = GetValue(SqlReader, 7, typeOfColumn.GetString)
                    mLinkType = GetValue(SqlReader, 8, typeOfColumn.GetInt32)
                    mOrderPosition = GetValue(SqlReader, 9, typeOfColumn.GetInt32)
                    mGoodMenuNo_ = GetValue(SqlReader, 10, typeOfColumn.GetString)
                    mGoodMenuOrder = GetValue(SqlReader, 11, typeOfColumn.GetInt32)
                    mIsGroup = GetValue(SqlReader, 12, typeOfColumn.GetInt32)
                    mParentNo_ = GetValue(SqlReader, 13, typeOfColumn.GetString)
                    mLoginStatus = GetValue(SqlReader, 14, typeOfColumn.GetInt32)
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
                arParams(2) = DBHelper.createParameter("@PositionNo_", SqlDbType.NVarChar, ParameterDirection.Input, mPositionNo_)
                arParams(3) = DBHelper.createParameter("@GroupNo_", SqlDbType.NVarChar, ParameterDirection.Input, mGroupNo_)
                arParams(4) = DBHelper.createParameter("@URLLink", SqlDbType.NVarChar, ParameterDirection.Input, mURLLink)
                arParams(5) = DBHelper.createParameter("@ClassCSS", SqlDbType.NVarChar, ParameterDirection.Input, mClassCSS)
                arParams(6) = DBHelper.createParameter("@Float", SqlDbType.Int, ParameterDirection.Input, mFloat)
                arParams(7) = DBHelper.createParameter("@Imgsrc", SqlDbType.NVarChar, ParameterDirection.Input, mImgsrc)
                arParams(8) = DBHelper.createParameter("@LinkType", SqlDbType.Int, ParameterDirection.Input, mLinkType)
                arParams(9) = DBHelper.createParameter("@OrderPosition", SqlDbType.Int, ParameterDirection.Input, mOrderPosition)
                arParams(10) = DBHelper.createParameter("@GoodMenuNo_", SqlDbType.NVarChar, ParameterDirection.Input, mGoodMenuNo_)
                arParams(11) = DBHelper.createParameter("@GoodMenuOrder", SqlDbType.Int, ParameterDirection.Input, mGoodMenuOrder)
                arParams(12) = DBHelper.createParameter("@IsGroup", SqlDbType.Int, ParameterDirection.Input, mIsGroup)
                arParams(13) = DBHelper.createParameter("@ParentNo_", SqlDbType.NVarChar, ParameterDirection.Input, mParentNo_)
                arParams(14) = DBHelper.createParameter("@LoginStatus", SqlDbType.Int, ParameterDirection.Input, mLoginStatus)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_LinkMenu", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function GetMenu(Optional ByVal sCoditions As String = "") As DataTable
            Dim pSQL As String
            pSQL = " SELECT * FROM [Link Menu] WHERE 1 = 1 "
            If sCoditions.Trim <> "" Then
                pSQL &= " AND " & sCoditions
            End If
            pSQL &= " ORDER BY [Order Position]"
            Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, pSQL).Tables(0)
        End Function
#End Region

    End Class
End Namespace