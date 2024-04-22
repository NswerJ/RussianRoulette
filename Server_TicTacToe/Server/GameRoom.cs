using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Session;
using ServerCore;

namespace Server
{
    class GameRoom
    {
        List<ClientSession> _sessions = new List<ClientSession>();
        object _lock = new object();

        public void Move(ClientSession session, C_MoveStone packet)
        {
            lock (_lock)
            {
                // 돌 받고
                session.StonePosition = packet.select;

                // 특정 유저에게 보냄 // 수정필요 : sessionID를 못불러오고 있음 수정해야함!!!
                foreach (ClientSession s in _sessions)
                {   // 목적지id와 수신할 게임룸 플레이어id 일치시
                    Console.WriteLine($"SessionID :{s.SessionId}");
                    if (s.SessionId == packet.destinationId)
                    {
                        Console.WriteLine($"셀렉트 : {packet.select}");
                        Console.WriteLine($"목적지id : {packet.destinationId}");
                        Console.WriteLine($"SessionID :{s.SessionId}");
                        Console.WriteLine($"StonePosition :{session.StonePosition}");

                        S_MoveStone SMove = new S_MoveStone();
                        SMove.select = session.StonePosition;
                        s.Send(SMove.Write());    // 목적지id 유저(세션)에게 전송
                    }
                }
                
            }
        }


        public void Enter(ClientSession session)
        {
            lock(_lock)
            {   // 신규 유저 추가
                _sessions.Add(session);
                Console.WriteLine($"세션 추가 : {session.SessionId}");

                session.Room = this;
                
                // 신규 유저 접속시, 기존 유저 목록 전송
                S_PlayerList players = new S_PlayerList();
                foreach (ClientSession s in _sessions)
                {
                    players.players.Add(new S_PlayerList.Player() {
                        isSelf = (s == session),
                        playerId = s.SessionId,
                    });
                }
                session.Send(players.Write());

                // 신규 유저 접속 전체 공지
                S_BroadcastEnterGame enter = new S_BroadcastEnterGame();
                enter.playerId = session.SessionId;
                BroadCast(enter.Write());
            }

        }

        public void Leave(ClientSession session)
        {
            lock (_lock)
            {
                // 플레이어 제거하고
                _sessions.Remove(session);

                // 모두에게 알린다
                S_BroadcastLeaveGame leave = new S_BroadcastLeaveGame();
                leave.playerId = session.SessionId;
                BroadCast(leave.Write());
            }
        }

        public void BroadCast(ArraySegment<byte> segment) 
        {
            ArraySegment<byte> packet = segment;

            lock (_lock) // 
            {
                foreach(ClientSession s in _sessions)
                {
                    s.Send(segment);    // 리스트에 들어있는 모든 클라에 전송
                }
            }
        }

    }
}
