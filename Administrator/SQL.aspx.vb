
Partial Class Administrator_SQL
    Inherits System.Web.UI.Page

    Protected Sub UpdateLinkItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UpdateLinkItem.Click
        Dim objItem As New adv.Business.Item
        objItem.UpdateAllLinkItem()
    End Sub
End Class
