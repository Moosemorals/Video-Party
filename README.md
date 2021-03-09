# Video Party

Husband and a couple of friends get together online once
a week to watch a video. I want to build them a simple
(to use) system that replicates their current setup.

Their current setup is based on them all having copies
of the same video file(s). They use a tool that synchronises
VLC for them, and run that next to a desktop video conference
so they can talk about the video they're watching.

## Requirements

 1. Show a video

 2. Synchronise the video (at least play/pause/seek, ideally
    tracking position and speeding up/slowing down clients
    to keep people on the same frame)

  3. Video conference - stream video from each user to each
    other user (n^2 streams, although there's only 3 or 4 of
    them/us)

HTML5 gives us (1), WebRTC (and some messing about) gives us 
(3), which leaves (2) to me.

## Thoughts

A basic version, where video commands are passsed through websockets
from a client to the server, and then on to other clients, is 
easy enough to build, but will have timing issues.

The system needs to measure latency from the server to each client
and then calculate when to send commands so that they reach clients
at the same time. Since latancy is dynamic, need to track it without
burning too much resources. Will also need to make sure I understand
where any buffering is happening and what control I have over it.


