create or replace package Pkm_Tournament as

	type tab_type is varray(6) of Element.type%type;

	:resultat tab_type;
	
	truc number := 0;
	
	function get_type(nom_pkm Pokemon.nom%type) return tab_type;
	
end Pkm_Tournament;
/


create or replace package body Pkm_Tournament as
	
	function get_type (nom_pkm Pokemon.nom%type)
	return tab_type
	is get_type_resu tab_type := tab_type('', '', '', '', '', '');
	
	maximum number(2, 1);
	
	begin
		
		select count(nom)
		into maximum
		from type_final
		where nom = nom_pkm
		group by nom;
		
		for i in 1..maximum loop
			select type
			into get_type_resu(i)
			from type_final
			where nom = nom_pkm;
		end loop;

		return get_type_resu;
		
	end get_type;
end Pkm_Tournament;
/ 
		
