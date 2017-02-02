
/* -----------------------------------------------------------------------------
* Fichier : bd_config.sql
* Date    : 02/2017
*
* Desc    : def initiale d'une base de donnee pour l'app pokemon tournament.
*           fichier en 2 parties -- creation des tables, remplissage basique.
*
* Rmq     : on dit que l'on fonctionne par "liste blanche" lorsque pour une
*           variable donnee on specifie les valeurs qu'elle peut prendre.
*           Si on avait specifie les valeurs qu'elle ne peut pas prendre au
*           contraire, on aurait parle de "liste noire".
*
----------------------------------------------------------------------------- */

/* Creation des tables ------------------------------------------------------ */

create table Element (
    ide  int not null,                /* id element */
    type char(20),                    /* type element */
    constraint pk_elt primary key (ide),
    constraint uniq_t_e unique(type),
    constraint tp_chk check (type IN ('Eau', 'Feu', 'Sol', 'Plante', 'Electrique', 'Glace', 'Vol'))
    /* tp_chk restreint le champ d'assignation du type, on
    * fonctionne sur le principe de la liste blanche.
    * -------------------------
    * on met type unique pour qu'il n'y ait qu'un seul elt de chaque type
    * On garde l'id car plus facile a faire apparaitre en clef etrangere.
    * * * * * * * * * * * */
);


create table Phase (
    idp int not null,                /* id phase */
    typp char(20) not null,                   /* type de phase */
    constraint pk_phs primary key (idp),
    constraint uniq_t_p unique(typp),
    constraint ph_chk check (typp IN ('HuitiemeFinale', 'QuartFinale', 'DemiFinale', 'Finale'))
    /* ph_chk restreint le champ d'assignation du typp, on
    * fonctionne sur le principe de la liste blanche.
    * ------------------------
    * meme chose pouse idp et typp.
    * * * * * * * * */
);


create table Pokemon (
    idp     int not null,                 /* id pokemon */
    nom     varchar(32),                  /* nom pokemon */
    vie     int,                          /* vie du pokemon */
    force   int,                          /* force du pokemon */
    defense int,                          /* defense poke */
    type    int,                          /* type pokemon (par son id) */
    constraint pk_poke primary key (idp),
    constraint sk_type_poke foreign key (type) references Element(ide)
);


create table Stade (
    ids  int not null,                    /* id stade */
    nbp  int,                             /* nombre places stade */
    nom  varchar(32),                     /* nom du stade */
    type int,                             /* type terrain (par son id) */
    constraint pk_stade primary key (ids),
    constraint sk_type_std foreign key (type) references Element(ide)
);


create table Match (
    idm int not null,                /* id match */
    pk1 int,                         /* id pokemon 1 */
    pk2 int,                         /* id pokemon 2 */
    pkv int,                         /* id pokemon vainqueur */
    std int,                         /* id stade */
    phs int,                         /* id phase */
    constraint pk_match primary key (idm),
    constraint sk_poke1 foreign key (pk1) references Pokemon(idp),
    constraint sk_poke2 foreign key (pk2) references Pokemon(idp),
    constraint sk_phase foreign key (phs) references Phase(idp),
    constraint chk_poke check (pk1 != pk2)
    /* sk_poke1 et sk_poke2 traduisent la composition --> 1 stade
    *  contient 2 pokemons. On le traduit en relationnel avec des
    *  clefs etrangeres
    * -------------
    *  chk_poke assure qu'on ne peut avoir un match d'un pokemon
    *  contre lui-meme
    * * * * * * * * * * * * * * * * * */
);


create table Utilisateur (
    idu      int not null,                /* id user */
    nom      varchar(32),                 /* nom user */
    prenom   varchar(32),                 /* prenom user */
    login    varchar(32),                 /* login user */
    password varchar(32),                 /* mdp user */
    constraint pk_utilisateur primary key (idu)
);


/* Remplissage de base ------------------------------------------------------ */

/* Dans chacune des tables on specifie l'id manuellement. Mais au vu des valeurs
*  donnees, l'auto_increment aurait fait le memme boulot strictement. Question
*  temporaire de clarte.
*
* Il faudrait trouver comment fonctionne auto_increment, prends ptet des param
* * * * * * * * * * * * * * */


/* ==== creation elements ==== */
insert into Element (ide, type)
values (1, 'Eau');

insert into Element (ide, type)
values (2, 'Feu');

insert into Element (ide, type)
values (3, 'Sol');

insert into Element (ide, type)
values (4, 'Plante');

insert into Element (ide, type)
values (5, 'Electrique');

insert into Element (ide, type)
values (6, 'Glace');

insert into Element (ide, type)
values (7, 'Vol');


/* ==== creation phase ==== */
insert into Phase (idp, typp)
values (1, 'HuitiemeFinale');

insert into Phase (idp, typp)
values (2, 'QuartFinale');

insert into Phase (idp, typp)
values (3, 'DemiFinale');

insert into Phase (idp, typp)
values (4, 'Finale');


/* ==== ajout de pokemons ==== */
insert into Pokemon (idp, nom, vie, force, defense, type)
values (1, 'Electhor', 200, 75, 30, 7);

insert into Pokemon (idp, nom, vie, force, defense, type)
values (2, 'Brasegali', 160, 90, 25, 2);

insert into Pokemon (idp, nom, vie, force, defense, type)
values (3, 'Onigali', 180, 55, 45, 6);

insert into Pokemon (idp, nom, vie, force, defense, type)
values (4, 'Lucario', 165, 70, 35, 3);

insert into Pokemon (idp, nom, vie, force, defense, type)
values (5, 'Tortank', 220, 60, 30, 1);

insert into Pokemon (idp, nom, vie, force, defense, type)
values (6, 'Germinion', 140, 65, 25, 4);

insert into Pokemon (idp, nom, vie, force, defense, type)
values (7, 'Groudon', 200, 85, 35, 2);

insert into Pokemon (idp, nom, vie, force, defense, type)
values (8, 'Kyogre', 210, 75, 35, 1);


/* ==== ajout de stades ==== */
insert into Stade (ids, nom, nbp, type)
values (1, 'Stade neutre', 75000, 1); /* /!\ pas logique (1 = eau != neutre ) */

insert into Stade (ids, nom, nbp, type)
values (2, 'Stade éclair', 50000, 5);

insert into Stade (ids, nom, nbp, type)
values (3, 'Stade aquatique', 90000, 1);

insert into Stade (ids, nom, nbp, type)
values (4, 'Stade volcan', 60000, 2);


/* ==== ajout de matchs ==== */
insert into Match (idm, pk1, pk2, pkv, phs, std)
values (1, 1, 2, 1, 2, 2);

insert into Match (idm, pk1, pk2, pkv, phs, std)
values (2, 3, 4, 3, 2, 1);

insert into Match (idm, pk1, pk2, pkv, phs, std)
values (3, 5, 6, 5, 2, 3);

insert into Match (idm, pk1, pk2, pkv, phs, std)
values (4, 7, 8, 7, 2, 4);

insert into Match (idm, pk1, pk2, pkv, phs, std)
values (5, 1, 3, 1, 3, 4);

insert into Match (idm, pk1, pk2, pkv, phs, std)
values (6, 5, 7, 5, 3, 2);

insert into Match (idm, pk1, pk2, pkv, phs, std)
values (7, 1, 5, 5, 4, 1);

/* == rappel :
*        pkv --> pokemon victorieux
*        phs --> phase du tournoi
*        std --> id du stade
* * * * * * * * * * * * * * * */


/* ==== ajout d'utilisateurs ==== */

/* l'auteur de ce fichier tient a signaler que 3h se sont ecoulees depuis qu'il
* a commence a faire cette bor*** de base de donnees de ses ... pantoufles ...
* hum ... Tout ca pour dire qu'il s'est accorde de placer des prenoms d'users
* qu'il trouve poetique.
*                                         Cordialement, la direction.         */

/* rmq : les mdp seront a hasher */
insert into Utilisateur (idu, nom, prenom, login, password)
values (1, 'Peano', 'Alizarine', 'Al1z', '0000');

insert into Utilisateur (idu, nom, prenom, login, password)
values (2, 'Godel', 'Jezabel', 'J3zz', '0000');

insert into Utilisateur (idu, nom, prenom, login, password)
values (3, 'Russel', 'Neige', 'N31g', '0000');

insert into Utilisateur (idu, nom, prenom, login, password)
values (4, 'Hilbert', 'Aurore', 'Aur0', '0000');

insert into Utilisateur (idu, nom, prenom, login, password)
values (5, 'Frege', 'Fleur', 'Fl3u', '0000');

insert into Utilisateur (idu, nom, prenom, login, password)
values (6, 'Whitehead', 'Lily', 'L1l1', '0000');

insert into Utilisateur (idu, nom, prenom, login, password)
values (7, 'Dedekind', 'Andromede', 'Andr', '0000');

insert into Utilisateur (idu, nom, prenom, login, password)
values (8, 'Cantor', 'Anne-Lyse', 'Ann3', '0000');


/* EOF ---------------------------------------------------------------------- */
