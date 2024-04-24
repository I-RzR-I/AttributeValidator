To initialize use available attributes you must install the package.

 After installation, in your data model, just decorate properties with the necessary attributes.
 
 In current revision, are available:
  * `ValGreaterThanAttribute`;
  * `ValRequiredNotDefaultAttribute`;
  * `ValRequiredNotEmptyAttribute`;
  * `ValRequiredNotNullAttribute`;
  * `ValRequiredPositiveAttribute`.
  
  Also you can use abailable validation from `System.ComponentModel.DataAnnotations`.
  
  ```csharp
public class DataModel
   {
        [ValRequiredNotNull]
        [ValRequiredNotDefault]
        public int IntNumber { get; set; }

        [ValRequiredPositive]
        [ValGreaterThan(5)]
        [ValRequiredNotDefault]
        public long LongNumber { get; set; }

        [ValRequiredPositive]
        [ValGreaterThan(10)]
        public short ShortNumber { get; set; }

        [ValGreaterThan(1)]
        public ushort UnSignedShortNumber { get; set; }

        [ValGreaterThan(5.69)]
        public decimal DecimalNumber { get; set; }

        [ValGreaterThan(6.9)]
        public float FloatNumber { get; set; }

        [ValRequiredPositive]
        [ValRequiredNotDefault]
        [ValGreaterThan(56.9)]
        public double DoubleNumber { get; set; }
        
        [ValRequiredNotNull]
        public string StringProp { get; set; }
   }
```
