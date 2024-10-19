namespace CombatAssistant.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GivenTwoPlayersInCombat_HigherInitiativePlayerActsFirst()
    {
        var combatProcessor = new CombatEncounter();
        var playerOne = combatProcessor.EnterCombat("playerOne", 55);
        var playerTwo = combatProcessor.EnterCombat("playerTwo", 35);

        var activePlayer = combatProcessor.GetActivePlayer();
        
        Assert.That(activePlayer, Is.SameAs(playerOne));
    }

    [Test]
    public void GivenTwoPlayersInCombat_WhenFirstPlayerEndsTurn_SecondPlayerIsActive()
    {
        var combatProcessor = new CombatEncounter();
        var playerOne = combatProcessor.EnterCombat("playerOne", 55);
        var playerTwo = combatProcessor.EnterCombat("playerTwo", 35);

        playerOne.EndTurn();
        var activePlayer = combatProcessor.GetActivePlayer();
        
        Assert.That(activePlayer, Is.SameAs(playerTwo));
    }

    public void Test()
    {
        var combatProcessor = new CombatEncounter();
        var playerOne = combatProcessor.EnterCombat("playerOne", 55);
        var playerTwo = combatProcessor.EnterCombat("playerTwo", 35);
        
        
    }
}