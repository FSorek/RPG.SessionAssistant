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
        var combatProcessor = new CombatProcessor();
        var playerOne = new Player("Gerlach");
        var playerTwo = new Player("Robor Tinson");
        int p1Initiative = 55;
        int p2Initiative = 30;

        combatProcessor.EnterCombat(playerOne, p1Initiative);
        combatProcessor.EnterCombat(playerTwo, p2Initiative);
        var activePlayer = combatProcessor.GetActivePlayer();
        
        Assert.That(activePlayer, Is.SameAs(playerOne));
    }

    [Test]
    public void GivenTwoPlayersInCombat_WhenFirstPlayerEndsTurn_SecondPlayerIsActive()
    {
        var combatProcessor = new CombatProcessor();
        var playerOne = new Player("Gerlach");
        var playerTwo = new Player("Robor Tinson");
        int p1Initiative = 55;
        int p2Initiative = 30;

        var p1Combatant = combatProcessor.EnterCombat(playerOne, p1Initiative);
        var p2Combatant = combatProcessor.EnterCombat(playerTwo, p2Initiative);
        p1Combatant.EndTurn();
        var activePlayer = combatProcessor.GetActivePlayer();
        
        Assert.That(activePlayer, Is.SameAs(playerTwo));
    }
}