﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Jotunn.Managers;
using Jotunn.Utils;

namespace JotunnDoc.Docs
{
    public class PieceDoc : Doc
    {
        public PieceDoc() : base("pieces/piece-list.md")
        {
            PieceManager.OnPiecesRegistered += docPieces;
        }

        public void docPieces()
        {
            if (Generated)
            {
                return;
            }

            Jotunn.Logger.LogInfo("Documenting pieces");

            AddHeader(1, "Piece list");
            AddText("All of the pieces currently in the game.");
            AddText("This file is automatically generated from Valheim using the JotunnDoc mod found on our GitHub.");
            AddTableHeader("Piece Table", "Prefab", "Token", "Name", "Description", "Resources required");

            var pieceTables = ReflectionHelper.GetPrivateField<Dictionary<string, PieceTable>>(PieceManager.Instance, "PieceTableMap");

            foreach (var pair in pieceTables)
            {
                foreach (GameObject obj in pair.Value.m_pieces)
                {
                    Piece piece = obj.GetComponent<Piece>();

                    if (piece == null)
                    {
                        continue;
                    }

                    string resources = "<ul>";

                    foreach (Piece.Requirement req in piece.m_resources)
                    {
                        resources += $"<li>{req.m_amount} {JotunnDoc.Localize(req?.m_resItem?.m_itemData?.m_shared?.m_name)}</li>";
                    }

                    resources += "</ul>";

                    AddTableRow(
                        pair.Key,
                        obj.name,
                        piece.m_name,
                        JotunnDoc.Localize(piece.m_name),
                        JotunnDoc.Localize(piece.m_description),
                        resources);
                }
            }

            Save();
        }
    }
}
