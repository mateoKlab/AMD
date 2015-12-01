using System.Collections;
using System.Xml.Serialization;

[XmlRoot]
public class TownData {

    /// <summary>
    /// The party's capacity depends on this facilities level.
    /// </summary>
    [XmlElement]
    public int barracksLevel = 1;

    /// <summary>
    /// The fighter's training regimen and effects depends on this facilities level.
    /// </summary>
    [XmlElement]
    public int trainingGroundsLevel = 0;

    /// <summary>
    /// Weapon level depends on this facilities level.
    /// </summary>
    [XmlElement]
    public int workShopLevel = 0;
}
