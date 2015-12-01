using System.Collections;
using System.Xml.Serialization;

[XmlRoot]
public class TownData {

    [XmlIgnore]
    public const int BASE_BARRACKS_LEVEL = 1;
    [XmlIgnore]
    public const int MAX_BARRACKS_LEVEL = 4;
    [XmlIgnore]
    public const int BASE_TRAINING_GROUNDS_LEVEL = 0;
    [XmlIgnore]
    public const int MAX_TRAINING_GROUNDS_LEVEL = 1;
    [XmlIgnore]
    public const int BASE_WORKSHOP_LEVEL = 0;
    [XmlIgnore]
    public const int MAX_WORKSHOP_LEVEL = 1;
    [XmlIgnore]
    public const int BASE_MAXIMUM_FIGHTER_SLOTS = 50;
    [XmlIgnore]
    public const int BASE_PARTY_CAPACITY = 20;

    /// <summary>
    /// The maximum number of fighters depends on this facilities level.
    /// </summary>
    [XmlElement]
    public int barracksLevel = BASE_BARRACKS_LEVEL;

    /// <summary>
    /// The fighter's training regimen and effects depends on this facilities level.
    /// </summary>
    [XmlElement]
    public int trainingGroundsLevel = BASE_TRAINING_GROUNDS_LEVEL;

    /// <summary>
    /// Weapon level depends on this facilities level.
    /// </summary>
    [XmlElement]
    public int workshopLevel = BASE_WORKSHOP_LEVEL;

    #region Facility Effects

    // Barracks Effect
    public int AdditionalFighterCapacity()
    {
        return (barracksLevel * 10);
    }

    public int AdditionalPartyCapacity()
    {
        return (barracksLevel * 5);
    }

    #endregion
}
