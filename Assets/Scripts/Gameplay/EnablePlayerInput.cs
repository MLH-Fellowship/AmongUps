using Platformer.Core;
using Platformer.Model;
using Platformer.Mechanics;

namespace Platformer.Gameplay
{
    /// <summary>
    /// This event is fired when user input should be enabled.
    /// </summary>
    public class EnablePlayerInput : Simulation.Event<EnablePlayerInput>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        public PlayerController player;

        public override void Execute()
        {
            player.controlEnabled = true;
        }
    }
}