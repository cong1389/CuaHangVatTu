Imports System.Data
Imports Microsoft.VisualBasic

Namespace adv.Business
    Public Class ShowListLine

#Region "Khai báo"

        Private mDocumentNo_ As String = ""
        Private mLineNo_ As Integer = 0
        Private mItemNo_ As String = ""
        Private mMenuNo_ As String = ""
        Private mFullImgUrl As String = ""
        Private mThumbImgUrl As String = ""
        Private mPositionOrder As Integer = 0

        Property DocumentNo_() As String
            Get
                Return mDocumentNo_
            End Get
            Set(ByVal value As String)
                mDocumentNo_ = value
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
        Property ItemNo_() As String
            Get
                Return mItemNo_
            End Get
            Set(ByVal value As String)
                mItemNo_ = value
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
        Property FullImgUrl() As String
            Get
                Return mFullImgUrl
            End Get
            Set(ByVal value As String)
                mFullImgUrl = value
            End Set
        End Property
        Property ThumbImgUrl() As String
            Get
                Return mThumbImgUrl
            End Get
            Set(ByVal value As String)
                mThumbImgUrl = value
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

        Public Function Save() As Boolean
            Dim arParams() As IDataParameter = New IDataParameter(6) {}
            Try
                arParams(0) = DBHelper.createParameter("@DocumentNo_", SqlDbType.NVarChar, ParameterDirection.Input, mDocumentNo_)
                arParams(1) = DBHelper.createParameter("@LineNo_", SqlDbType.Int, ParameterDirection.Input, mLineNo_)
                arParams(2) = DBHelper.createParameter("@ItemNo_", SqlDbType.NVarChar, ParameterDirection.Input, mItemNo_)
                arParams(3) = DBHelper.createParameter("@MenuNo_", SqlDbType.NVarChar, ParameterDirection.Input, mMenuNo_)
                arParams(4) = DBHelper.createParameter("@FullImgUrl", SqlDbType.NVarChar, ParameterDirection.Input, mFullImgUrl)
                arParams(5) = DBHelper.createParameter("@ThumbImgUrl", SqlDbType.NVarChar, ParameterDirection.Input, mThumbImgUrl)
                arParams(6) = DBHelper.createParameter("@PositionOrder", SqlDbType.Int, ParameterDirection.Input, mPositionOrder)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_Show List Line", arParams)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
#End Region

    End Class

End Namespace