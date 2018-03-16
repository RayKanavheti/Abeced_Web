
function toggleClass(x , classname){
    if($(x).hasClass(classname)){
        $(x).removeClass(classname);
    }else{
        $(x).addClass(classname);
    }     
}
function toggleMenu(){
    $("#div-side").toggle();
    x = $(".main-contents")[0];
    toggleClass(x , "removed_side");
    toggleClass(x , "container-fluid");
}