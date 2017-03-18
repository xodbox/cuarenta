using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedCuarenta.Naipes;
using SharedCuarenta.Utilities;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace SharedCuarenta.CuarentaEngine
{
    class PartidaDeCuarenta
    {
        #region Properties
        public GrupoDeNaipes[] Manos { get; }
        public GrupoDeNaipes NaipesEnMesa { get; }
        public GrupoDeNaipes Mazo { get; }
        public GrupoDeNaipes Perros { get; }
        public GrupoDeNaipes[] Carton { get; }
        public GrupoDeNaipes[] Puntos { get; }
        public bool oneInHandSelected { get; set; }
        public bool oneInMesaTouched { get; set; }
        #endregion

        #region Fields
        List<Naipe> CollectedFromTable = new List<Naipe>();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor of the elements of a Cuarenta Match
        /// </summary>
        public PartidaDeCuarenta()
        {
            Manos = new GrupoDeNaipes[4];
            NaipesEnMesa = new GrupoDeNaipes();
            Mazo = new GrupoDeNaipes();
            Perros = new GrupoDeNaipes();
            Carton = new GrupoDeNaipes[2];
            Puntos = new GrupoDeNaipes[2];
            for (int i = 0; i < 4; i++)
                Manos[i] = new GrupoDeNaipes();
            for (int i = 0; i < 2; i++)
                Carton[i] = new GrupoDeNaipes();
            for (int i = 0; i < 2; i++)
                Puntos[i] = new GrupoDeNaipes();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Begin a Cuarenta Match, clearing everything, shuffleing the deck and dealing the first hand.
        /// </summary>
        /// <param name="numeroDeJugadores">Number of players in the match</param>
        /// <param name="thisPlayer">Which player is this</param>
        /// <param name="dataPlayer">Which player is dealing</param>
        public void IniciarPartida(int numeroDeJugadores, int thisPlayer, int dataPlayer)
        {
            if (numeroDeJugadores != 2 && numeroDeJugadores != 4)
                throw new ArgumentOutOfRangeException();

            Perros.NaipesEnGrupo.Clear();

            foreach (CardPalo palo in Enum.GetValues(typeof(CardPalo)))
            {
                foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
                {
                    if (rank > CardRank.Siete && rank < CardRank.Jota)
                    {
                        Perros.AnadirArriba(new Naipe(rank, palo));
                        Perros.NaipesEnGrupo[Perros.NaipesEnGrupo.Count - 1].FaceUp = false;
                        Perros.NaipesEnGrupo[Perros.NaipesEnGrupo.Count - 1].OnGame = false;
                    }                        
                }
            }
            Perros.Barajar();
            IniciarNuevaData(numeroDeJugadores, thisPlayer, dataPlayer);
        }

        public void IniciarNuevaData(int numeroDeJugadores, int thisPlayer, int dataPlayer)
        {
            if (numeroDeJugadores != 2 && numeroDeJugadores != 4)
                throw new ArgumentOutOfRangeException();

            for (int i = 0; i < 4; i++)
            {
                Manos[i].NaipesEnGrupo.Clear();
            }
            NaipesEnMesa.NaipesEnGrupo.Clear();
            Mazo.NaipesEnGrupo.Clear();

            foreach (CardPalo palo in Enum.GetValues(typeof(CardPalo)))
            {
                foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
                {
                    if (rank <= CardRank.Siete || rank >= CardRank.Jota)
                    {
                        Mazo.AnadirArriba(new Naipe(rank, palo));
                        Mazo.NaipesEnGrupo[Mazo.NaipesEnGrupo.Count - 1].FaceUp = false;
                        Mazo.NaipesEnGrupo[Mazo.NaipesEnGrupo.Count - 1].OnGame = true;
                    }
                }
            }
            Mazo.Barajar();
            Repartir(numeroDeJugadores, thisPlayer, dataPlayer);
        }

        /// <summary>
        /// Deals a hand of Cuarenta
        /// </summary>
        /// <param name="numeroDeJugadores">Numbers of players in the Match</param>
        /// <param name="thisPlayer">Which player is this</param>
        /// <param name="dataPlayer">Which player is dealing</param>
        public void Repartir(int numeroDeJugadores, int thisPlayer, int dataPlayer)
        {
            if (numeroDeJugadores != 2 && numeroDeJugadores != 4)
                throw new ArgumentOutOfRangeException();

            int indexPlayer = dataPlayer + 1;
            for (int i = 0; i < numeroDeJugadores; i++)
            {
                if (indexPlayer >= numeroDeJugadores)
                    indexPlayer = 0;
                for (int j = 0; j < 5; j++)
                {
                    Manos[indexPlayer].AnadirArriba(Mazo.TomarTop());
                    if (thisPlayer == indexPlayer)
                    {
                        Manos[indexPlayer].NaipesEnGrupo[j].FaceUp = true;
                    }
                    else
                        Manos[indexPlayer].NaipesEnGrupo[j].FaceUp = false;
                }
                indexPlayer++;
            }
        }

        /// <summary>
        /// Process if the player has clicked a card in their hand
        /// </summary>
        /// <param name="clickedPoint">Point of the window clicked</param>
        /// <param name="thisPlayer">Player who is playing</param>
        /// <returns>Returns true if a card in the hand of the player was clicked</returns>
        public bool ProcessCardInHandClicked(Point clickedPoint, int thisPlayer)
        {
            bool clicked = false;
            foreach (Naipe card in Manos[thisPlayer].NaipesEnGrupo)
            {
                if (card.isClicked(clickedPoint.X, clickedPoint.Y))
                {
                    if (card.Selected)
                    {
                        card.MoveToOriginalPosition();
                        card.Selected = false;
                        oneInHandSelected = false;
                    }
                    else
                    {
                        foreach (Naipe everyCard in Manos[thisPlayer].NaipesEnGrupo)
                        {
                            everyCard.MoveToOriginalPosition();
                            everyCard.Selected = false;
                        }
                        card.MoveUp();
                        card.Selected = true;
                        oneInHandSelected = true;
                    }
                    clicked = true;
                }
            }
            return clicked;
        }

        /// <summary>
        /// Process if one card in the table (Mesa) has been clicked
        /// Also sets the property if at least one card is touched (selected)
        /// </summary>
        /// <param name="clickedPoint">Point where the player has clicked</param>
        /// <returns>Returns true if a card in the table (Mesa) has been clicked</returns>
        public bool ProcessCardInMesaClicked(Point clickedPoint)
        {
            bool clicked = false;
            oneInMesaTouched = false;

            foreach (Naipe card in NaipesEnMesa.NaipesEnGrupo)
            {
                if (card.isClicked(clickedPoint.X, clickedPoint.Y))
                {
                    if (card.Touched)
                    {
                        card.Touched = false;
                    }
                    else
                    {
                        card.Touched = true;
                    }
                    clicked = true;
                }
                if (card.Touched)
                {
                    oneInMesaTouched = true;
                }
            }
            return clicked;
        }

        /// <summary>
        /// This method is used when the user trhrows a card in the table
        /// </summary>
        /// <param name="clickedPoint">where the user clicked on the table</param>
        /// <param name="thisPlayer">who is playing</param>
        /// <param name="tableLimits">limits of the playing table</param>
        /// <param name="selectedCard">which card is been trown</param>
        /// <param name="cardSlots">spaces allowed to place cards</param>
        /// <returns>returns true if a valid place on the playing table was clicked</returns>
        public bool ProcessMesaClicked (
            Point clickedPoint, 
            int thisPlayer, 
            Rectangle tableLimits, 
            Naipe selectedCard, 
            CardSlots cardSlots)
        {
            if (tableLimits.Contains(clickedPoint.X, clickedPoint.Y))
            {
                NaipesEnMesa.NaipesEnGrupo.Add(selectedCard);
                Manos[thisPlayer].NaipesEnGrupo.Remove(selectedCard);
                selectedCard.Selected = false;
                oneInHandSelected = false;
                for (int i = 0; i < 10; i++)
                {
                    if (!cardSlots.UsedTableCardPosition[i])
                    {
                        selectedCard.SetCenter(cardSlots.TableCardPosition[i]);
                        cardSlots.UsedTableCardPosition[i] = true;
                        selectedCard.SlotAssigned = CardSlots.getGroupIndexCardsInTable(i);
                        break;
                    }
                }
                return true;
            }
            return false;
        }

        public bool MakeMove(int thisPlayer, int thisTeam, CardSlots cardSlots)
        {
            List<Naipe> touchedCardsOnTable = new List<Naipe>();
            Naipe selectedCardOnHand = new Naipe();

            foreach (Naipe card in NaipesEnMesa.NaipesEnGrupo)
            {
                if (card.Touched)
                {
                    touchedCardsOnTable.Add(card);
                }
            }

            foreach (Naipe card in Manos[thisPlayer].NaipesEnGrupo)
            {
                if (card.Selected)
                {
                    selectedCardOnHand = card;
                    break;
                }
            }

            foreach (Naipe card in Manos[thisPlayer].NaipesEnGrupo)
            {
                card.Selected = false;
                card.MoveToOriginalPosition();
            }
            oneInHandSelected = false;

            foreach (Naipe card in NaipesEnMesa.NaipesEnGrupo)
                card.Touched = false;
            oneInMesaTouched = true;

            touchedCardsOnTable.Sort(new NaipesComparer());
            if(ProcessMove(selectedCardOnHand, touchedCardsOnTable))
            {
                Manos[thisPlayer].NaipesEnGrupo.Remove(selectedCardOnHand);
                foreach (Naipe card in touchedCardsOnTable)
                {
                    Pair<CardGroup, int> slot = card.SlotAssigned;
                    cardSlots.UsedTableCardPosition[slot.Second]= false;
                    NaipesEnMesa.NaipesEnGrupo.Remove(card);
                }

                selectedCardOnHand.SetCenter(cardSlots.CartonCardPosition[thisTeam, 0]);
                selectedCardOnHand.SlotAssigned = CardSlots.getGroupIndexCartonCards(thisTeam, 0);
                Carton[thisTeam].AnadirArriba(selectedCardOnHand);
                foreach (Naipe card in touchedCardsOnTable)
                {
                    card.SetCenter(cardSlots.CartonCardPosition[thisTeam, 0]);
                    card.SlotAssigned = CardSlots.getGroupIndexCartonCards(thisTeam, 0);
                    Carton[thisTeam].AnadirArriba(card);
                }

                return true;
            }
            else
            {
                foreach (Naipe card in Manos[thisPlayer].NaipesEnGrupo)
                    card.Selected = false;
                foreach (Naipe card in NaipesEnMesa.NaipesEnGrupo)
                    card.Touched = false;

                return false;
            }
        }
        #endregion

        #region Private Methods
        private bool ProcessMove(Naipe selectedCardOnHand, List<Naipe> touchedCardsOnTable)
        {
            int rankVal;

            for (int i = 0; i < touchedCardsOnTable.Count; i++)
            {
                if (selectedCardOnHand.Rank == touchedCardsOnTable[0].Rank)
                {
                    if (i == 0)
                    {
                        CollectedFromTable.Add(selectedCardOnHand);
                        CollectedFromTable.Add(touchedCardsOnTable[0]);
                    }
                    if (i > 0)
                    {
                        rankVal = (int)touchedCardsOnTable[i].Rank;
                        if (rankVal >= 11)
                            rankVal -= 3;

                        if ((int)touchedCardsOnTable[i - 1].Rank + 1 == rankVal)
                            CollectedFromTable.Add(touchedCardsOnTable[i]);
                        else
                            return false;
                    }
                }
                else if (
                    touchedCardsOnTable.Count >= 2 &&
                    (int)touchedCardsOnTable[0].Rank <= 7 &&
                    (int)touchedCardsOnTable[1].Rank <= 7 &&
                    (int)selectedCardOnHand.Rank == (int)touchedCardsOnTable[0].Rank + (int)touchedCardsOnTable[1].Rank)
                {
                    if (i == 0)
                    {
                        CollectedFromTable.Add(selectedCardOnHand);
                        CollectedFromTable.Add(touchedCardsOnTable[0]);
                        CollectedFromTable.Add(touchedCardsOnTable[1]);
                    }
                    if (i > 1)
                    {
                        rankVal = (int)touchedCardsOnTable[i].Rank;
                        if (rankVal >= 11)
                            rankVal -= 3;

                        if ((int)touchedCardsOnTable[i - 1].Rank + 1 == rankVal)
                            CollectedFromTable.Add(touchedCardsOnTable[i]);
                        else
                            return false;
                    }
                }
                else
                    return false;
            }
            return true;
        }
        #endregion
    }
}
