﻿using System;
using ChainReact.Core;
using ChainReact.UI.Base;
using ChainReact.UI.Extensions;
using ChainReact.UI.Types;
using Sharpex2D.Framework;
using Sharpex2D.Framework.Rendering;
using Sharpex2D.Framework.UI;

namespace ChainReact.UI
{
    public class Checkbox : Control, ICheckableControl
    {
        public Texture2D[] Textures { get; protected set; }
        public bool IsChecked { get; set; }

        public event EventHandler OnCheckedChanged;

        public Checkbox(Game game, string checkedTex, string uncheckedTex, string text, string font) : base(game)
        {
            Textures = new Texture2D[4];
            Textures[0] = ResourceManager.GetResource<Texture2D>(checkedTex);
            Textures[1] = ResourceManager.GetResource<Texture2D>(uncheckedTex);
            Textures[2] = ResourceManager.GetResource<Texture2D>(checkedTex).Grayscale();
            Textures[3] = ResourceManager.GetResource<Texture2D>(uncheckedTex).Grayscale();
            Text = text;
            Font = ResourceManager.GetResource<SpriteFont>(font);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime time)
        {
            if (!Visible) return;
            var tex = (IsChecked) ? Textures[0] : Textures[1];
            tex = (Enabled) ? tex : (IsChecked) ? Textures[2] : Textures[3];
            spriteBatch.DrawTexture(tex, new Rectangle(Position.X, Position.Y, Size.Width, Size.Height), Color.White);
            spriteBatch.DrawString(Text, Font, new Vector2(Position.X + 35, Position.Y + 5), Color);
        }

        public virtual void Checked(GameTime time)
        {
            if (!Enabled) return;
            IsChecked = !IsChecked;
            OnCheckedChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
