if($#ARGV==-1)
{
	printf "To prepare RNA profiles you have to have x3dna-dssr\n";
	printf "This program should be installed in the same directory as the current perl script\n";
	printf "Tu run program specify as the argument the name of the directory where the RNA pdb files are\n";
	printf "For example:\n";
	printf "\t\tperl dssr.pl 1a4d_pdbs\n";
	printf "The results will be stored in the file 1a4d_pdbs_dssr\n";	
	exit;
}
if(!(-f x3dna-dssr))
{
	printf "x3dna-dssr seem's to be not installed!!\n";
	exit;
}
if(!(-d $ARGV[0]))
{
	printf "Incorrect directory name $ARGV[0]\n";
	exit;
}


$dirName=$ARGV[0];
%profile=();
@files=glob("$dirName/*.pdb");
if(scalar(@files)==0)
{
	printf "There are no .pdb files in the specified director\n";
	exit;
}

foreach $n (@files)
{
	DSSR($n);		
}
$outFile=$m."_dssr";
open(file_out,">$outFile") or die "File $outFile cannot be open";
	
foreach $m (keys %profile)
{
	printf file_out "$m.pdb\n";
	printf file_out "RNA SEQ $profile{$m}{SEQ}\n";
	printf file_out "RNA SS $profile{$m}{SS}\n";
	printf file_out "RNA TOR $profile{$m}{TORSION}\n";
}
close(file_out);
	

sub DSSR
{
	my $fileName=shift;


	system("./x3dna-dssr -i=$fileName -o=test 2>/dev/null");
	
	open(file_in,"dssr-2ndstrs.dbn");
	$k=<file_in>;
	@tmp=split(/ /,$k);
	$name=$tmp[0];
	$k=<file_in>;
	chomp($k);
	$seq=$k;
	$seq=~s/&//g;
	$k=<file_in>;
	chomp($k);
	$profile=$k;
	$profile=~s/&//g;
	close(file_in);
	
	$profile{$name}{SEQ}=$seq;
	$profile{$name}{SS}=$profile;
	$profile{$name}{TORSION}=TorsionFile("dssr-torsions.txt");
	
	
}
sub TorsionFile
{
	my $fileName=shift;
	my $profile;
	open(file_in,$fileName) or die "File $fileName cannot be open";
	$k=<file_in>;
	while($k)
	{
		$k=~s/\t+/ /g;
		$k=~s/ +/ /g;
		$k=~s/^ //;
		@tmp=split(/ /,$k);
		if($k=~/^nt/)
		{
			last;			
		}
		$k=<file_in>;
	}
	$k=<file_in>;
	while($k && !($k=~/\*\*/))
	{
		$k=~s/\t+/ /g;
		$k=~s/ +/ /g;
		$k=~s/^ //;
		@tmp=split(/ /,$k);
		if($tmp[9]=~/--/ || $tmp[8]=~/--/)
		{
			$profile.="N";
		}
		elsif($tmp[9]=~/anti/)
		{
			if($tmp[8]=~/BII/)
			{
				$profile.="K";
			}
			else
			{
				$profile.="A";
			}
		}
		else
		{
			if($tmp[8]=~/BII/)
			{
				$profile.="S";
			}
			else
			{
				$profile.="D";
			}
		}
		
		$k=<file_in>;
	}
	
	
	
	close(file_in);
	#printf "$profile\n";
	return $profile;
}
	
