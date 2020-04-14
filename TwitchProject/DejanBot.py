import Settings
from twitchio.ext import commands
import os
import time


bot = commands.Bot(
    irc_token=Settings.TMI_TOKEN,
    client_id=Settings.CLIENT_ID,
    nick = Settings.BOT_NICK,
    prefix= Settings.BOT_PREFIX,
    initial_channels=[Settings.CHANNEL]
)



@bot.event
async def event_ready():
    'Called once when the bot goes online.'
    print(f"{Settings.BOT_NICK} is online!")
    ws = bot._ws  # this is only needed to send messages within event_ready
    #await ws.send_privmsg(Settings.CHANNEL, f"/me has landed!")

@bot.event
async def event_message(ctx):
    'Runs every time a message is sent in chat.'

    print(f'{ctx.author.name}: {ctx.content}')
    # make sure the bot ignores itself and the streamer
    if ctx.author.name.lower() == Settings.BOT_NICK.lower():
        await bot.handle_commands(ctx)




# bot.py
if __name__ == "__main__":
    bot.run()
