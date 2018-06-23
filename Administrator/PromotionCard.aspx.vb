Imports System.Data

Partial Class Administrator_PromotionCard
    Inherits System.Web.UI.Page
    Dim objPromotion As New adv.Business.Promotion
    Dim sPromotionNo As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            sPromotionNo = ReturnIfNull(Request("promotionno"), "").ToString.Trim
            BuildCombo(DrdBrand, adv.Business.List.Brand, "", True, "")
            BuildCombo(CboMenuGroup, adv.Business.List.MenuGroup, "", True, "")
            If sPromotionNo.Trim <> "" Then
                ShowData(sPromotionNo)
            End If
        End If
    End Sub

    Sub ShowData(ByVal sPromotionNo As String)
        objPromotion.Load(sPromotionNo)
        With objPromotion
            TxtNo_.Text = .No_
            TxtName.Text = .Name
            TxtStartingDate.Text = Char2Date(.StartingDate)
            TxtEndingDate.Text = Char2Date(.EndingDate)
            CboDiscountType.SelectedValue = .PromotionType
            TxtItemNo.Text = .ItemNo_
            DrdBrand.SelectedValue = .BrandNo_
            CKPublished.Checked = IIf(.Published = 1, True, False)
            TxtContent.Value = .Description
            CboMenuGroup.SelectedValue = .MenuNo
        End With
    End Sub

    Protected Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Response.Redirect("PromotionsList.aspx")
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            objPromotion.Load(TxtNo_.Text)
            With objPromotion
                .No_ = TxtNo_.Text
                .Name = TxtName.Text
                .StartingDate = Date2Char(TxtStartingDate.Text)
                .EndingDate = Date2Char(TxtEndingDate.Text)
                .PromotionType = CboDiscountType.SelectedValue
                .ItemNo_ = TxtItemNo.Text
                .BrandNo_ = DrdBrand.SelectedValue
                .Published = IIf(CKPublished.Checked, 1, 0)
                .Description = TxtContent.Value
                .MenuNo = CboMenuGroup.SelectedValue
                .Save()
                Response.Redirect("PromotionsList.aspx")
            End With
        Catch ex As Exception
            LblWarning.Text = ex.Message
        End Try
    End Sub
End Class
