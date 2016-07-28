@files=glob('/home/raad/work/rna/rnaDB2005/*');
$counter=0;
$modelNum=0;
$resNum=7;

foreach $m (@files)
{
		printf ("$m");
		ReadRNA($m);
}

sub ReadRNA
{
	my $fileName=shift;
	my @P=();
	
	$n=0;
	$resCount=1;
	$atomCount=1;
	
	open(file_in,$fileName);
	$k=<file_in>;
	while($k)
	{
		if($k=~/^ATOM/ || $k=~/^TER/)
		{
		$m=$k;
		$m=~s/\t/ /g;
		$m=~s/ +/ /g;
		@tmp=split(/ /,$m);
		if($tmp[2]=~/^P/ || $k=~/TER/)
		{
			push @P,$k;
		}
#		if($tmp[2]=~/^N/)
#		{
#			printf "$k";
#			push @N,$k;	
#		}
		
		}
		$k=<file_in>;
	}
	for($i=0;$i<scalar(@P)-$resNum;$i++)
	{
		
		
		$xt="";
		for($j=$i;$j<$i+$resNum;$j++)
		{
			substr($P[$j],19,1)="G";
			$txt.=$P[$j];
			if($P[$j]=~/^TER/)
			{
				$txt="";
			}
			
		}
		if(length($txt)>0)
		{
			$name="/home/raad/work/rna/frags7/RNAfrag".$modelNum++.".pdb";
			open(file_out,">$name") or die "File $name cannot be open";
			printf file_out $txt;
			close(file_out);
		}
	}
	
	
	close(file_in);
}
sub Average
{
	my $tab=shift;
	
	my @pos=();
	
	foreach $i (@{$tab})
	{
		$m=$i;
		$m=~s/\t/ /g;
		$m=~s/ +/ /g;
		
		@tt=split(/ /,$m);
		
		$pos[0]+=$tt[5];
		$pos[1]+=$tt[6];
		$pos[2]+=$tt[7];
		
		printf "$tt[5] $tt[6] $tt[7]\n";
		
	}
	for($i=0;$i<scalar(@pos);$i++)
	{
		$pos[$i]/=scalar(@{$tab});
	}
		
	return @pos;
	
}
