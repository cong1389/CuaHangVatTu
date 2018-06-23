Imports Microsoft.VisualBasic
Imports System.Data

Namespace adv.Business
    Public Class Brand
#Region "Khai báo"
        Private mNo_ As String = ""
        Private mName As String = ""
        Private mImageURL As String = ""
        Private mLastDateModify As String = ""
        Private mUserID As String = ""

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
        Property ImageURL() As String
            Get
                Return mImageURL
            End Get
            Set(ByVal value As String)
                mImageURL = value
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

#End Region
#Region "Function"
        Public Function Load(ByVal sNo_ As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Brand] WHERE No_ = '" & sNo_ & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mName = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mImageURL = GetValue(SqlReader, 2, typeOfColumn.GetString)
                    mLastDateModify = GetValue(SqlReader, 3, typeOfColumn.GetString)
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
                arParams(0) = DBHelper.createParameter("@No_", SqlDbType.NVarChar, ParameterDirection.Input, mNo_)
                arParams(1) = DBHelper.createParameter("@Name", SqlDbType.NVarChar, ParameterDirection.Input, mName)
                arParams(2) = DBHelper.createParameter("@ImageURL", SqlDbType.NVarChar, ParameterDirection.Input, mImageURL)
                arParams(3) = DBHelper.createParameter("@LastDateModify", SqlDbType.NVarChar, ParameterDirection.Input, mLastDateModify)
                arParams(4) = DBHelper.createParameter("@UserID", SqlDbType.NVarChar, ParameterDirection.Input, mUserID)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_Brand", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

#End Region

    End Class
End Namespace