Imports Microsoft.VisualBasic
Imports System.Data

Namespace adv.Business
    Public Class ItemDescriptions
#Region "Khai báo"
        Private mItemNo_ As String = ""
        Private mDateModify As String = ""
        Private mUserID As String = ""
        Private mContent As String = ""

        Property ItemNo_() As String
            Get
                Return mItemNo_
            End Get
            Set(ByVal value As String)
                mItemNo_ = value
            End Set
        End Property
        Property DateModify() As String
            Get
                Return mDateModify
            End Get
            Set(ByVal value As String)
                mDateModify = value
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
        Property Content() As String
            Get
                Return mContent
            End Get
            Set(ByVal value As String)
                mContent = value
            End Set
        End Property

#End Region
#Region "Function"
        Public Function Load(ByVal sItemNo As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Item Descriptions] WHERE [Item No_] = '" & sItemNo & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mItemNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mDateModify = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mUserID = GetValue(SqlReader, 2, typeOfColumn.GetString)
                    mContent = GetValue(SqlReader, 3, typeOfColumn.GetString)
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
                arParams(1) = DBHelper.createParameter("@DateModify", SqlDbType.NVarChar, ParameterDirection.Input, mDateModify)
                arParams(2) = DBHelper.createParameter("@UserID", SqlDbType.NVarChar, ParameterDirection.Input, mUserID)
                arParams(3) = DBHelper.createParameter("@Content", SqlDbType.NVarChar, ParameterDirection.Input, mContent)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_ItemDescriptions", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
#End Region

    End Class

End Namespace