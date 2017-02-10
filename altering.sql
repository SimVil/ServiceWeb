/* Modif des tables : ajout d'une image ------------------------------------------------------ */
alter table Pokemon
	add img_path char(100);
	
alter table Stade
	add img_path char(100);