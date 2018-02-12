An example of a population sampler which simulates the implementation of two Mark-Recapture methods, the Lincoln-Peterson and Schnabel Indexes, to show their accuracy. This is intended as an example simulation to show use, not to replicate empyrical data.

Takes Arguments:<br/>
  Int Sample Size: The overall size of the population to be sampled<br/>
  Int Sample Upper Limit: The maximum subjects in the sample to be captured at a time<br/>
  Int Sample Lower Limit: The minimum subjects in the sample to be captured at a time<br/>
  Int Amount of Samples: The amount of samples to be taken<br/>
  
This program takes in a the above parameters and uses the Mark-Recapture technique to estimate a populations total size. This program then calculates the Lincoln-Peterson and Schnabel Indexes and, if done correctly, should demonstrate how closely these indexes can represent a population.

Some errors can occur in the random number generation where the same number is generated multiple times within the same experiment for captures, therefore skewing the results of the indices. Currently this bug is non-replicable, but could still appear in future releases.

