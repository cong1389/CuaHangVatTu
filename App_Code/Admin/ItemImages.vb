Imports Microsoft.VisualBasic
Imports System.Data

Namespace adv.Business
    Public Class ItemImages
#Region "Khai báo"
        Private mItemNo_ As String = ""
        Private mLineNo_ As Integer = 0
        Private mImageThumbs As String = ""
        Private mImageNatural As String = ""

        Property ItemNo_() As String
            Get
                Return mItemNo_
            End Get
            Set(ByVal value As String)
                mItemNo_ = value
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
        Property ImageThumbs() As String
            Get
                Return mImageThumbs
            End Get
            Set(ByVal value As String)
                mImageThumbs = value
            End Set
        End Property
        Property ImageNatural() As String
            Get
                Return mImageNatural
            End Get
            Set(ByVal value As String)
                mImageNatural = value
            End Set
        End Property

#End Region
#Region "Function"
        Public Function Load(ByVal sItemNo As String, ByVal nLineNo As Integer) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Item Images] WHERE [Item No_] = '" & sItemNo & "' AND [Line No_] = " & nLineNo
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mItemNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mLineNo_ = GetValue(SqlReader, 1, typeOfColumn.GetInt32)
                    mImageThumbs = GetValue(SqlReader, 2, typeOfColumn.GetString)
                    mImageNatural = GetValue(SqlReader, 3, typeOfColumn.GetString)
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
            Dim arParams() As IDataParameter = New IDataParameter(3) {}
            Try
                arParams(0) = DBHelper.createParameter("@ItemNo_", SqlDbType.NVarChar, ParameterDirection.Input, mItemNo_)
                arParams(1) = DBHelper.createParameter("@LineNo_", SqlDbType.Int, ParameterDirection.Input, mLineNo_)
                arParams(2) = DBHelper.createParameter("@ImageThumbs", SqlDbType.NVarChar, ParameterDirection.Input, mImageThumbs)
                arParams(3) = DBHelper.createParameter("@ImageNatural", SqlDbType.NVarChar, ParameterDirection.Input, mImageNatural)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_ItemImages", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Sub CreateNew(ByVal sItemNo As String)
            Dim SQL As String
            Dim sImgName As String = ""
            Dim nLineNo As Integer
            SQL = "SELECT ISNULL(MAX([Line No_]),0) + 1 FROM [Item Images] WHERE [Item No_] = '" & sItemNo & "'"
            nLineNo = ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), 0)
            mItemNo_ = sItemNo
            mLineNo_ = nLineNo
            mImageNatural = sItemNo & "-" & nLineNo
            mImageThumbs = mImageNatural & "-T"
        End Sub

#End Region

    End Class
End Namespace