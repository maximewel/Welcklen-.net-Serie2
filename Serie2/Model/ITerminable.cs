using System;
using System.Collections.Generic;
using System.Text;

namespace Serie2.controler
{
    interface ITerminable
    {
        /// <summary>
        /// Query wether the game is finished
        /// </summary>
        /// <returns>True if the game is finished</returns>
        public Boolean IsFinished();
    }
}
