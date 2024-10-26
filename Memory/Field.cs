using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    public class Field
    {
        private Button Button {  get; set; }
        //add sound to animate

        public Field(Button button)
        {
            Button = button;
        }

        public async void AnimateButtonClick()
        {
            await Button.FadeTo(1.0, 250, Easing.CubicOut);
            await Button.FadeTo(0.5, 250, Easing.CubicIn);
        }
    }
}
