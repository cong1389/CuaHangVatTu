Imports Microsoft.VisualBasic
Imports System.Data

Namespace adv.Business

    Public Class ContentGroup
#Region "Khai báo"
        Private mGroupNo_ As String = ""
        Private mGroupName As String = ""
        Private mContentType As Integer = 0
        Private mCategoryNo_ As String = ""
        Private mLinkMenu As String = ""
        Private mParentLink As String = ""
        Private mContentDefault As String = ""

        Property GroupNo_() As String
            Get
                Return mGroupNo_
            End Get
            Set(ByVal value As String)
                mGroupNo_ = value
            End Set
        End Property
        Property GroupName() As String
            Get
                Return mGroupName
            End Get
            Set(ByVal value As String)
                mGroupName = value
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
        Property CategoryNo_() As String
            Get
                Return mCategoryNo_
            End Get
            Set(ByVal value As String)
                mCategoryNo_ = value
            End Set
        End Property
        Property LinkMenu() As String
            Get
                Return mLinkMenu
            End Get
            Set(ByVal value As String)
                mLinkMenu = value
            End Set
        End Property
        Property ParentLink() As String
            Get
                Return mParentLink
            End Get
            Set(ByVal value As String)
                mParentLink = value
            End Set
        End Property
        Property ContentDefault() As String
            Get
                Return mContentDefault
            End Get
            Set(ByVal value As String)
                mContentDefault = value
            End Set
        End Property

#End Region
#Region "Function"
        Public Function Load(ByVal sNo As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Content Group] WHERE [Group No_] = '" & sNo & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mGroupNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mGroupName = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mContentType = GetValue(SqlReader, 2, typeOfColumn.GetInt32)
                    mCategoryNo_ = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mLinkMenu = GetValue(SqlReader, 4, typeOfColumn.GetString)
                    mParentLink = GetValue(SqlReader, 5, typeOfColumn.GetString)
                    mContentDefault = GetValue(SqlReader, 6, typeOfColumn.GetString)
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

        Public Function LoadByLinkMenu(ByVal sLink As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Content Group] WHERE [Link Menu] = '" & sLink & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mGroupNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mGroupName = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mContentType = GetValue(SqlReader, 2, typeOfColumn.GetInt32)
                    mCategoryNo_ = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mLinkMenu = GetValue(SqlReader, 4, typeOfColumn.GetString)
                    mParentLink = GetValue(SqlReader, 5, typeOfColumn.GetString)
                    mContentDefault = GetValue(SqlReader, 6, typeOfColumn.GetString)
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
                arParams(0) = DBHelper.createParameter("@GroupNo_", SqlDbType.NVarChar, ParameterDirection.Input, mGroupNo_)
                arParams(1) = DBHelper.createParameter("@GroupName", SqlDbType.NVarChar, ParameterDirection.Input, mGroupName)
                arParams(2) = DBHelper.createParameter("@ContentType", SqlDbType.Int, ParameterDirection.Input, mContentType)
                arParams(3) = DBHelper.createParameter("@CategoryNo_", SqlDbType.NVarChar, ParameterDirection.Input, mCategoryNo_)
                arParams(4) = DBHelper.createParameter("@LinkMenu", SqlDbType.NVarChar, ParameterDirection.Input, mLinkMenu)
                arParams(5) = DBHelper.createParameter("@ParentLink", SqlDbType.NVarChar, ParameterDirection.Input, mParentLink)
                arParams(6) = DBHelper.createParameter("@ContentDefault", SqlDbType.NVarChar, ParameterDirection.Input, mContentDefault)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_ContentGroup", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function GetContentTypeByLink(ByVal sLinkType As String) As Integer
            Dim SQL As String
            SQL = " SELECT No_ FROM [Content Type] WHERE [Link Menu] = '" & sLinkType & "'"
            Return DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL)
        End Function

        Public Function GetNameContentTypeByLink(ByVal sLinkType As String) As String
            Dim SQL As String
            SQL = " SELECT Name FROM [Content Type] WHERE [Link Menu] = '" & sLinkType & "'"
            Return DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL)
        End Function
#End Region

    End Class

End Namespace
