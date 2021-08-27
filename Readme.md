<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128582500/10.1.7%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E2688)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [Form1.cs](./CS/WindowsFormsSample/Form1.cs) (VB: [Form1.vb](./VB/WindowsFormsSample/Form1.vb))
<!-- default file list end -->
# How to serialize PivotGridControl descendant's properties with the control's layout


<p><strong>NOTE: This example was created for older XtraPivotGrid versions. The demonstrated solution is appropriate for versions prior to v2011.1.</strong><strong><br />
</strong><br />
Generally, to serialize a property of the DevExpress control, it is enough to mark it with the XtraSerializableProperty attribute (see <a href="https://www.devexpress.com/Support/Center/p/K18435">How to serialize a custom property of the DevExpress control's descendant</a>). However, this approach will not work in PivotGridControl since the PivotGridData object, not the PivotGridControl itself, is serialized. So, to accomplish this task, it is required to define the property in PivotGridData, and refer to this property in PivotGridControl. Please see the example's code to see how this should be implemented.</p><p><strong>Starting with version 11.1, it is sufficient to decorate properties declared in the PivotGridControl descendant with the XtraSerializableProperty attribute:</strong></p>

```cs
public class MyPivotGrid : PivotGridControl, IXtraSupportDeserializeCollectionItem {
    private List<Class1> _Objects = new List<Class1>();
    [XtraSerializableProperty(XtraSerializationVisibility.Collection, true)]
    public List<Class1> Objects {
        get {
            return _Objects;
        }
    }
    private Class1 _Object = new Class1();
    [XtraSerializableProperty(XtraSerializationVisibility.Content)]
    public Class1 Object {
        get { return _Object; }
    }
    #region IXtraSupportDeserializeCollectionItem Members
    object IXtraSupportDeserializeCollectionItem.CreateCollectionItem(string propertyName, XtraItemEventArgs e) {
        if (propertyName == "Objects") {
            Class1 class1 = new Class1();
            Objects.Add(class1);
            return class1;
        } else return null;
    }

    void IXtraSupportDeserializeCollectionItem.SetIndexCollectionItem(string propertyName, XtraSetItemIndexEventArgs e) {

    }
    #endregion
}
```

<p> </p>

<br/>


