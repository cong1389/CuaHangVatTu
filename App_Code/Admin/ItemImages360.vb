Imports Microsoft.VisualBasic
Imports System.Data


Namespace adv.Business
    Public Class ItemImages360
#Region "Khai báo"
        Private mItemNo_ As String = ""
        Private mLineNo_ As Integer = 0
        Private mPostionOrder As Integer = 0
        Private mImagesURL As String = ""
        Private mUserID As String = ""

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
        Property PostionOrder() As Integer
            Get
                Return mPostionOrder
            End Get
            Set(ByVal value As Integer)
                mPostionOrder = value
            End Set
        End Property
        Property ImagesURL() As String
            Get
                Return mImagesURL
            End Get
            Set(ByVal value As String)
                mImagesURL = value
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

#End Region
#Region "Function"
        Public Function Load(ByVal pKhoa As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Item Images 360] WHERE Khoa = '" & pKhoa & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mItemNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mLineNo_ = GetValue(SqlReader, 1, typeOfColumn.GetInt32)
                    mPostionOrder = GetValue(SqlReader, 2, typeOfColumn.GetInt32)
                    mImagesURL = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mUserID = GetValue(SqlReader, 4, typeOfColumn.GetString)
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
            Dim arParams() As IDataParameter = New IDataParameter(4) {}
            Try
                arParams(0) = DBHelper.createParameter("@ItemNo_", SqlDbType.NVarChar, ParameterDirection.Input, mItemNo_)
                arParams(1) = DBHelper.createParameter("@LineNo_", SqlDbType.Int, ParameterDirection.Input, mLineNo_)
                arParams(2) = DBHelper.createParameter("@PostionOrder", SqlDbType.Int, ParameterDirection.Input, mPostionOrder)
                arParams(3) = DBHelper.createParameter("@ImagesURL", SqlDbType.NVarChar, ParameterDirection.Input, mImagesURL)
                arParams(4) = DBHelper.createParameter("@UserID", SqlDbType.NVarChar, ParameterDirection.Input, mUserID)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_ItemImages360", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
#End Region

    End Class

End Namespace